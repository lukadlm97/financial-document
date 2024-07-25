using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocument.Persistance.Implementations;

public class ProductRepository(FinancialDocumentDbContext _financialDocumentDbContext) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _financialDocumentDbContext.Products.ToListAsync(cancellationToken);
    }
}
