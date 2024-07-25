using FinancialDocument.Domain.Entities;

namespace FinancialDocument.Application.Contracts.Repositories;
public interface IClientRepository
{
    Task<IReadOnlyList<Client>> GetAsync(CancellationToken cancellationToken = default);
}
