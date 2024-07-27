using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Domain.Entities;
using FinancialDocument.Infrastructure.Services;
using FinancialDocument.Infrastructure.UnitTests.Mocks;
using Moq;

namespace FinancialDocument.Infrastructure.UnitTests.Services;

public class TenantServiceTest
{
    private readonly Mock<IDataRepository<Tenant>> _tenantRepositoryMock;
    private readonly CancellationToken _token;

    public TenantServiceTest()
    {
        _tenantRepositoryMock = MockTenantRepository.GetTenantRepository();
        _token = CancellationToken.None;
    }

    [Theory]
    [InlineData("guid", true)]
    [InlineData("guid2", true)]
    [InlineData("test", false)]
    [InlineData("", false)]
    [InlineData("Product4", false)]
    public async Task IsSupportedAsync(string code, bool isSupported)
    {
        ITenantService tenantService = new TenantService(_tenantRepositoryMock.Object);

        var isWhitelisted = await tenantService.IsWhitelistedAsync(code, _token);

        if (isSupported)
        {
            Assert.True(isWhitelisted);
        }
        else
        {
            Assert.False(isWhitelisted);
        }
    }
}
