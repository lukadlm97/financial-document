namespace FinancialDocument.Application.Contracts.Repositories;
public interface IDataRepository<T> where T : class
{
    IQueryable<T> Get();
}
