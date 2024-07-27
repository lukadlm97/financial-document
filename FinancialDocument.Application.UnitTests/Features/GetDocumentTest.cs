using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Application.UnitTests.Mocks;
using FinancialDocument.Domain.Entities;
using FluentValidation;
using Moq;
using static FinancialDocument.Application.Feature.GetDocument.GetDocument;

namespace FinancialDocument.Application.UnitTests.Features;

public class GetDocumentTest
{
    private readonly Mock<IClientService> _clientServiceMock;
    private readonly Mock<ITenantService> _tenantServiceMock;
    private readonly Mock<IProductService> _productServiceMock;
    private readonly Mock<IDocumentFetcher> _documentServiceMock;
    private readonly Mock<IDataRepository<ProductProperty>> _productPropertyRepositreMock;
    private readonly CancellationToken _token;

    public GetDocumentTest()
    {
        _clientServiceMock = MockClientService.GetClientService();
        _tenantServiceMock = MockTenantService.GetTenantService();
        _productServiceMock = MockProductService.GetProductService();
        _documentServiceMock = MockDocumentFetcher.GetDocumentFetcher();
        _productPropertyRepositreMock = MockProductProperyRepository.GetProductProperyRepository();
        _token = CancellationToken.None;
    }

    [Theory]
    [InlineData("ProductA", "guid", "guid")]
    public async Task GetAsync_DocumentExist(string code, string tenantId, string documentId)
    {
        var request = new GetDocumentRequest(code, tenantId, documentId);
        var handler = new GetDocumentRequestHandler(_clientServiceMock.Object, _documentServiceMock.Object, _productPropertyRepositreMock.Object);

        var result = await handler.Handle(request, _token);

        Assert.NotNull(result);
    }

    [Theory]
    [InlineData("ProductA", "guid", "guid")]
    public async Task Validator_Success(string code, string tenantId, string documentId)
    {
        var request = new GetDocumentRequest(code, tenantId, documentId);
        var handler = new GetDocumentRequestValidator(_productServiceMock.Object, _tenantServiceMock.Object);

        var result = await handler.ValidateAsync(request);

        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("ProductC", "guid", "guid")]
    public async Task Validator_Error_ProductNotEnabled(string code, string tenantId, string documentId)
    {
        var request = new GetDocumentRequest(code, tenantId, documentId);
        var handler = new GetDocumentRequestValidator(_productServiceMock.Object, _tenantServiceMock.Object);

        var result = await handler.ValidateAsync(request);

        Assert.NotNull(result);
        Assert.False(result.IsValid);
    }
    [Theory]
    [InlineData("ProductD", "guid", "guid")]
    public async Task Validator_Error_ProductNotExist(string code, string tenantId, string documentId)
    {
        var request = new GetDocumentRequest(code, tenantId, documentId);
        var handler = new GetDocumentRequestValidator(_productServiceMock.Object, _tenantServiceMock.Object);

        var result = await handler.ValidateAsync(request);

        Assert.NotNull(result);
        Assert.False(result.IsValid);
    }
    [Theory]
    [InlineData("ProductA", "guid4", "guid")]
    public async Task Validator_Error_TenantNotWhitelisted(string code, string tenantId, string documentId)
    {
        var request = new GetDocumentRequest(code, tenantId, documentId);
        var handler = new GetDocumentRequestValidator(_productServiceMock.Object, _tenantServiceMock.Object);

        var result = await handler.ValidateAsync(request);

        Assert.NotNull(result);
        Assert.False(result.IsValid);
    }
    [Theory]
    [InlineData("ProductA", "guid2", "guid3")]
    public async Task GetAsync_ClientNotExist(string code, string tenantId, string documentId)
    {
        var request = new GetDocumentRequest(code, tenantId, documentId);
        var handler = new GetDocumentRequestHandler(_clientServiceMock.Object, _documentServiceMock.Object, _productPropertyRepositreMock.Object);
        
        Func<Task<GetDocumentResponse>> act = () =>  handler.Handle(request, _token);

        await Assert.ThrowsAsync<ValidationException>(act);
    }
    [Theory]
    [InlineData("ProductA", "guid2", "guid2")]
    public async Task GetAsync_SmallCompany(string code, string tenantId, string documentId)
    {
        var request = new GetDocumentRequest(code, tenantId, documentId);
        var handler = new GetDocumentRequestHandler(_clientServiceMock.Object, _documentServiceMock.Object, _productPropertyRepositreMock.Object);

        Func<Task<GetDocumentResponse>> act = () => handler.Handle(request, _token);

        await Assert.ThrowsAsync<ValidationException>(act);
    }
}
