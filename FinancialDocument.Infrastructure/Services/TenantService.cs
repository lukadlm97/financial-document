using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;

namespace FinancialDocument.Infrastructure.Services
{
    public class TenantService(ITenantRepository _tenantRepository) : ITenantService
    {
        public async Task<bool> IsWhitelistedAsync(string tenantId, CancellationToken cancellationToken = default)
        {
            return (await _tenantRepository.GetAsync(cancellationToken))
                                            .Any(x => x.UniqueIdentifier.Equals(tenantId) 
                                                        && x.IsWhitelisted);
        }
    }
}
