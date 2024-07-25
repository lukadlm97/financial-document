namespace FinancialDocument.Application.Contracts.Repositories;
public interface IDataEntry<T> where T : class
{
    IQueryable<T> Get();
}
