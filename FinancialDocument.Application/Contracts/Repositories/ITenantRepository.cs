using FinancialDocument.Domain.Entities;

namespace FinancialDocument.Application.Contracts.Repositories;
public interface ITenantRepository
{
    Task<IReadOnlyList<Tenant>> GetAsync(CancellationToken cancellationToken = default);
}
