using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Domain.Entities;

namespace FinancialDocument.Infrastructure.Services;
public class ProductService(IDataRepository<Product> _productRepository) : IProductService
{
    public async Task<bool> IsSupportedAsync(string code, CancellationToken cancellationToken = default)
    {
        return  _productRepository.Get()
                        .Any(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase)
                                    && x.IsEnabled);
}
}
