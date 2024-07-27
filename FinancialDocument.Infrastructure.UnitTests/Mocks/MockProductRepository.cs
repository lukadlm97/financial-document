using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using Moq;

namespace FinancialDocument.Infrastructure.UnitTests.Mocks;

internal class MockProductRepository
{
    public static Mock<IDataRepository<Product>> GetProductRepository()
    {
        var mockRepository = new Mock<IDataRepository<Product>>();


        var product1 = new Product() { Id = 1, Code = "ProductA", Name = "Product1", IsEnabled = true };
        var product2 = new Product() { Id = 2, Code = "ProductB", Name = "Product2", IsEnabled = true };
        var product3 = new Product() { Id = 3, Code = "ProductC", Name = "Product3", IsEnabled = false };
        var products = new List<Product>()
        {
            product1,
            product2,
            product3
        };


        mockRepository.Setup(r => r.Get())
            .Returns(products.AsQueryable);

        return mockRepository;
    }
}
