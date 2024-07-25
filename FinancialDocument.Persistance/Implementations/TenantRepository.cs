using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocument.Persistance.Implementations;

public class TenantRepository(FinancialDocumentDbContext _financialDocumentDbContext) : ITenantRepository
{
    public async Task<IReadOnlyList<Tenant>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await _financialDocumentDbContext.Tenants
                                                .Include(x=>x.Document)
                                                .ToListAsync(cancellationToken);
    }
}
