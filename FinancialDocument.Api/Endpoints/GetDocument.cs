using Microsoft.AspNetCore.Mvc;

using MediatR;

using FinancialDocument.Api.Extensions;
using GetDocumentRequest =  FinancialDocument.Application.Feature.GetDocument.GetDocument.GetDocumentRequest;
using GetDocumentRequestDto =  FinancialDocument.Api.DTOs.GetDocumentRequest;

namespace FinancialDocument.Api.Endpoints;

public class GetDocument : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost($"api/financialDocument", async ([FromServices] IMediator mediator, [FromBody] GetDocumentRequestDto request) =>
        {
            return await mediator.Send(new GetDocumentRequest(request.ProductCode, request.TenandId, request.DocumentId));
        });
    }
}
