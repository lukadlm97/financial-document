using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocument.Persistance.Implementations;

public class ClientRepository(FinancialDocumentDbContext _financialDocumentDbContext) : IClientRepository
{
    public async Task<IReadOnlyList<Client>> GetAsync(CancellationToken cancellationToken = default)
    { 
        return await _financialDocumentDbContext.Clients
                                                .Include(x=>x.Company)
                                                .ToListAsync(cancellationToken);
    }
}
