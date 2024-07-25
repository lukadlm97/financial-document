using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialDocument.Application.UnitTests.Mocks
{
    public class MockProductProperyRepository
    {
        public static Mock<IDataEntry<ProductProperty>> GetProductProperyRepository()
        {
            var mockRepository = new Mock<IDataEntry<ProductProperty>>();


            var products = new List<Product>()
        {
            new Product() { Id = 1, Code = "Product1", Name = "Product1", IsEnabled = true },
            new Product() { Id = 2, Code = "Product2", Name = "Product2", IsEnabled = true },
            new Product() { Id = 3, Code = "Product3", Name = "Product3", IsEnabled = true }
        };
            var productProperties = new List<ProductProperty>()
                {
                    new ProductProperty()
                    {
                         Product = new Product() { Id = 1, Code = "Product1", Name = "Product1", IsEnabled = true },
                         Property =  new Property(){ Id = 1, Name = "test1"},
                         PropertyRepresentationValue =  new PropertyRepresentationType(){},
                         PropertyRepresentationId = 1
                    },
                     new ProductProperty()
                    {
                         Product = new Product() { Id = 1, Code = "Product1", Name = "Product1", IsEnabled = true },
                         Property =  new Property(){ Id = 2, Name = "test2"},
                         PropertyRepresentationValue =  new PropertyRepresentationType(){},
                         PropertyRepresentationId = 2,
                         
                    },
                };

            mockRepository.Setup(r => r.Get())
                .Returns(productProperties.AsQueryable<ProductProperty>());

            return mockRepository;
        }
    }
}
