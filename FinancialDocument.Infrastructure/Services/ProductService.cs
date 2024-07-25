using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;

namespace FinancialDocument.Infrastructure.Services;

public class ProductService(IProductRepository _productRepository) : IProductService
{
    public async Task<bool> IsSupportedAsync(string code, CancellationToken cancellationToken = default)
    {
        return (await _productRepository.GetAsync(cancellationToken))
                                            .Any(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase)
                                                        && x.IsEnabled);
    }
}
