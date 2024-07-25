using FinancialDocument.Application.Contracts.Repositories;

namespace FinancialDocument.Persistance.Implementations;

public class DataEntry<T>(FinancialDocumentDbContext _financialDocumentDbContext) : IDataEntry<T> where T : class
{
    public IQueryable<T> Get()
    {
        return _financialDocumentDbContext.Set<T>();
    }
}
