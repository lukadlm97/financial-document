namespace FinancialDocument.Application.Contracts.Services;
public interface IProductService
{
    Task<bool> IsSupportedAsync(string code, CancellationToken cancellationToken = default);
}
