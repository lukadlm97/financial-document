using System.Collections.Immutable;

using FluentValidation;
using MediatR;

using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Application.Extensions;
using FinancialDocument.Application.Feature.Shared;
using FinancialDocument.Application.Contracts.DTOs;
using FinancialDocument.Domain.Entities;
using FinancialDocument.Domain.Exceptions;

namespace FinancialDocument.Application.Feature.GetDocument;

public class GetDocument
{
    public record GetDocumentRequest(string ProductCode, string TenantId, string DocumentId) : IRequest<GetDocumentResponse>;
    public record GetDocumentResponse(string Data, CompanyResponse Company);
    public sealed class GetDocumentRequestValidator : AbstractValidator<GetDocumentRequest>
    {
        public GetDocumentRequestValidator(IProductService productService, ITenantService tenantService)
        {
            // Validate Product Code:
            RuleFor(v => v.ProductCode)
                .MustAsync(productService.IsSupportedAsync)
                .WithMessage("Invalid Product Code parametar supplied.");


            // Tenant ID Whitelisting:
            RuleFor(v => v.TenantId)
                .MustAsync(tenantService.IsWhitelistedAsync)
                .WithMessage("Invalid Tenant Id parametar supplied.");
        }
    }
    public class GetDocumentRequestHandler(IClientService _clientService, 
                                            IDocumentFetcher _documentFetcher,
                                            IDataRepository<ProductProperty> _productPropertyRepository) : IRequestHandler<GetDocumentRequest, GetDocumentResponse>
    {
        public async Task<GetDocumentResponse> Handle(GetDocumentRequest request, CancellationToken cancellationToken)
        {
            // Client ID Whitelisting:
            var client = await _clientService.GetAsync(request.TenantId, request.DocumentId, cancellationToken);
            if(client == null || 
                !await _clientService.IsWhitelistedAsync(request.TenantId, client.Id, cancellationToken))
            {
                throw new ValidationException("Client for produced Tenant Id and Document Id doesnt exist or isn\'t whitelisted");
            }

            // Fetch Additional Client Information:
            var additionalClientInformation = await _clientService.GetDetailsAsync(client.Vat, cancellationToken);
            if(additionalClientInformation == null)
            {
                throw new ClientDetailsNotFoundException("Additional client information isn\'t supplied");
            }

            // Company Type Check:
            if (additionalClientInformation.CompanySize == Domain.Enums.CompanySize.Small)
            {
                throw new ValidationException("Company is too small to continue processing");
            }

            // Retrieve Financial Document for Client:
            var rawDocument = await _documentFetcher.Get(request.TenantId, request.DocumentId, cancellationToken);
            if (string.IsNullOrWhiteSpace(rawDocument))
            {
                throw new DocumentNotFoundException("Unable to retrieve financial document");
            }

            // Enrich Response Model:
            var companyResponse = new CompanyResponse(additionalClientInformation.RegistrationNumber, additionalClientInformation.CompanyType);

            // Financial Data Anonymization:
            var productProperties = _productPropertyRepository
                                        .Get()
                                        .Where(x => x.Product.Code.Equals(request.ProductCode, StringComparison.OrdinalIgnoreCase))
                                        .Select(x => new PropertySettings(x.PropertyRepresentationType, x.Property.Name))
                                        .ToList();
            var anonymizedJson = rawDocument.AnonymizeJson(productProperties);
            if (string.IsNullOrEmpty(anonymizedJson))
            {
                throw new ValidationException("Unable to parse document");
            }

            return new GetDocumentResponse(anonymizedJson, companyResponse);
        }
    }
}
