using FinancialDocument.Application.Contracts.DTOs;
using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Domain.Exceptions;
using FinancialDocument.Infrastructure.Services;
using FinancialDocument.Infrastructure.UnitTests.Mocks;
using Moq;

namespace FinancialDocument.Infrastructure.UnitTests.Services;

public class ClientServiceTest
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<ITenantRepository> _tenantRepositoryMock;
    private readonly CancellationToken _token;

    public ClientServiceTest()
    {
        _clientRepositoryMock = MockClientRepository.GetClientRepository();
        _tenantRepositoryMock = MockTenantRepository.GetTenantRepository();
        _token = CancellationToken.None;
    }

    [Theory]
    [InlineData("guid", "guid")]
    public async Task GetAsync_ClientExist(string tenantId, string documentId)
    {
        IClientService clientService = new ClientService(_tenantRepositoryMock.Object, _clientRepositoryMock.Object);

        var client = await clientService.GetAsync(tenantId, documentId, _token);

        Assert.NotNull(client);
        Assert.Equal("guid1", client.Id);
        Assert.Equal("test1", client.VAT);
    }

    [Theory]
    [InlineData("guid5", "guid5")]
    public async Task GetAsync_TenantNotExist(string tenantId, string documentId)
    {
        IClientService clientService = new ClientService(_tenantRepositoryMock.Object, _clientRepositoryMock.Object);

        var client = await clientService.GetAsync(tenantId, documentId, _token);

        Assert.Null(client);
    }

    [Theory]
    [InlineData("guid1", "guid4")]
    public async Task GetAsync_ClientNotExist(string tenantId, string documentId)
    {
        IClientService clientService = new ClientService(_tenantRepositoryMock.Object, _clientRepositoryMock.Object);

        var client = await clientService.GetAsync(tenantId, documentId, _token);

        Assert.Null(client);
    } 
    
    [Theory]
    [InlineData("guid", "guid")]
    public async Task IsWhitelistedAsync_Yes(string tenantId, string clientId)
    {
        IClientService clientService = new ClientService(_tenantRepositoryMock.Object, _clientRepositoryMock.Object);

        var result = await clientService.IsWhitelistedAsync(tenantId, clientId, _token);

        Assert.True(result);
    }

    [Theory]
    [InlineData("guid1", "guid4")]
    public async Task IsWhitelistedAsync_No(string tenantId, string clientId)
    {
        IClientService clientService = new ClientService(_tenantRepositoryMock.Object, _clientRepositoryMock.Object);

        var result = await clientService.IsWhitelistedAsync(tenantId, clientId, _token);

        Assert.False(result);
    }
    
    [Theory]
    [InlineData("test1")]
    public async Task GetDetailsAsync_ClientExist(string VAT)
    {
        IClientService clientService = new ClientService(_tenantRepositoryMock.Object, _clientRepositoryMock.Object);

        var result = await clientService.GetDetailsAsync(VAT, _token);

        Assert.NotNull(result);
        Assert.Equal("test1", result.RegistrationNumber);
        Assert.Equal("Small", result.CompanyType);
    }

    [Theory]
    [InlineData("test5")]
    public async Task GetDetailsAsync_ClientNotExist(string VAT)
    {
        IClientService clientService = new ClientService(_tenantRepositoryMock.Object, _clientRepositoryMock.Object);

        var result = await clientService.GetDetailsAsync(VAT, _token);

        Assert.Null(result);
    }


}
