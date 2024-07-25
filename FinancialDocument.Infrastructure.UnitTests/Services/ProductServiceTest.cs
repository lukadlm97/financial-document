using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Infrastructure.Services;
using FinancialDocument.Infrastructure.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialDocument.Infrastructure.UnitTests.Services
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly CancellationToken _token;

        public ProductServiceTest()
        {
            _productRepositoryMock = MockProductRepository.GetProductRepository();
            _token = CancellationToken.None;
        }

        [Theory]
        [InlineData("ProductA", true)]
        [InlineData("ProductB", true)]
        [InlineData("ProductC", false)]
        [InlineData("test", false)]
        [InlineData("", false)]
        [InlineData("Product4", false)]
        public async Task IsSupportedAsync_Yes(string code,bool isWhitelisted)
        {
            IProductService productService = new ProductService(_productRepositoryMock.Object);

            var isSupported = await productService.IsSupportedAsync(code, _token);

            if (isWhitelisted)
            {
                Assert.True(isSupported);
            }
            else
            {
                Assert.False(isSupported);
            }
        }
    }
}
