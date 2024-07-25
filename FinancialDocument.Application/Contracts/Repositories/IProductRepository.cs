using FinancialDocument.Domain.Entities;

namespace FinancialDocument.Application.Contracts.Repositories;
public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAsync(CancellationToken cancellationToken = default);
}
