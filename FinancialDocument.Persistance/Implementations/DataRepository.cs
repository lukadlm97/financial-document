using FinancialDocument.Application.Contracts.Repositories;

namespace FinancialDocument.Persistance.Implementations;
public class DataRepository<T>(FinancialDocumentDbContext _financialDocumentDbContext) 
    : IDataRepository<T> where T : class
{
    public IQueryable<T> Get()
    {
        return _financialDocumentDbContext.Set<T>();
    }
}
