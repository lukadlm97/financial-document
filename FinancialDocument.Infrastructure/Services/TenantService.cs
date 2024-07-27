using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Domain.Entities;

namespace FinancialDocument.Infrastructure.Services
{
    public class TenantService(IDataRepository<Tenant> _tenantRepository) : ITenantService
    {
        public async Task<bool> IsWhitelistedAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            return  _tenantRepository.Get()
                            .Any(x => x.UniqueIdentifier.Equals(tenantId, StringComparison.OrdinalIgnoreCase) 
                                        && x.IsWhitelisted);
        }
    }
}
