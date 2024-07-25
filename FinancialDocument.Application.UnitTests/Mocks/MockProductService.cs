using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Domain.Entities;
using Moq;

namespace FinancialDocument.Application.UnitTests.Mocks;

public class MockProductService
{
    public static Mock<IProductService> GetProductService()
    {
        var mockRepository = new Mock<IProductService>();

        var product1 = new Product() { Id = 1, Code = "ProductA", Name = "Product1", IsEnabled = true };
        var product2 = new Product() { Id = 2, Code = "ProductB", Name = "Product2", IsEnabled = true };
        var product3 = new Product() { Id = 3, Code = "ProductC", Name = "Product3", IsEnabled = false };
        var products = new List<Product>()
            {
                product1,
                product2,
                product3
            };

        mockRepository.Setup(r => r.IsSupportedAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string code, CancellationToken cancellationToken) =>
            {
                return products.Any(x => x.Code.Equals(code, StringComparison.OrdinalIgnoreCase) 
                                            && x.IsEnabled);
            });

        return mockRepository;
    }
}
