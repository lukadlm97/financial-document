using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialDocument.Application.UnitTests.Mocks
{
    internal class MockTenantService
    {
        public static Mock<ITenantService> GetTenantService()
        {
            var mockRepository = new Mock<ITenantService>();

            var document1 = new Document() { Id = 1, Name = "Document1", UniqueIdentifier = "guid" };
            var company3 = new Company() { Id = 3, Name = "Company3", RegistrationNumber = "test3", CompanySizeId = 3 };

            var tenants = new List<Tenant>()
            {
                new Tenant()
                {
                    Id = 1,
                    ClientId = 1,
                    DocumentId = 1,
                    IsWhitelisted = true,
                    UniqueIdentifier = "guid",
                    Client =  new Client() { Id = 1, Name = "Client1", UniqueIdentifier = "guid", VAT = "test1",
                    Company = new Company() { Id = 1, Name = "Company1", RegistrationNumber = "test1", CompanySizeId = 2 }},
                    Document =  document1,
                },
                new Tenant()
                {
                    Id = 2,
                    ClientId = 2,
                    DocumentId = 2,
                    IsWhitelisted = true,
                    UniqueIdentifier = "guid2",
                    Client=  new Client() { Id = 2, Name = "Client2", UniqueIdentifier = "guid2", VAT = "test2",
                    Company = new Company() { Id = 2, Name = "Company2", RegistrationNumber = "test2", CompanySizeId = 2 } },
                    Document =document1
                },
                new Tenant()
                {
                    Id = 3,
                    ClientId = 3,
                    DocumentId = 3,
                    IsWhitelisted = true,
                    UniqueIdentifier = "guid3",
                    Client =  new Client() { Id = 3, Name = "Client3", UniqueIdentifier = "guid3", VAT = "test3", Company = company3 },
                    Document = new Document() { Id = 2, Name = "Document2", UniqueIdentifier = "guid2" }
                },
                new Tenant()
                {
                    Id = 4,
                    ClientId = 1,
                    DocumentId = 1,
                    IsWhitelisted = false,
                    UniqueIdentifier = "guid4",
                    Client =    new Client() { Id = 4, Name = "Client4", UniqueIdentifier = "guid4", VAT = "test4", Company = company3 },
                    Document = new Document() { Id = 3, Name = "Document3", UniqueIdentifier = "guid3" }
                }
            };

            mockRepository.Setup(r => r.IsWhitelistedAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string tenantId, CancellationToken cancellationToken ) =>
                {
                    return tenants.Any(x=>x.UniqueIdentifier.Equals(tenantId, StringComparison.OrdinalIgnoreCase) && x.IsWhitelisted);   
                });

            return mockRepository;
        }
    }
}
