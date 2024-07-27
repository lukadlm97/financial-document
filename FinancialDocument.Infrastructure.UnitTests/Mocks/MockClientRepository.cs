using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using Moq;

namespace FinancialDocument.Infrastructure.UnitTests.Mocks;

public class MockClientRepository
{
    public static Mock<IDataRepository<Client>> GetClientRepository()
    {
        var mockRepository = new Mock<IDataRepository<Client>>();

        var company3 = new Company() { Id = 3, Name = "Company3", RegistrationNumber = "test3", CompanySizeId = 3 };
        var clients = new List<Client>()
        {
            new Client() { Id = 1, Name = "Client1", UniqueIdentifier = "guid1", Vat = "test1", Company = new Company() { Id = 1, Name = "Company1", RegistrationNumber = "test1", CompanySizeId = 1 }},
            new Client() { Id = 2, Name = "Client2", UniqueIdentifier = "guid2", Vat = "test2", Company = new Company() { Id = 2, Name = "Company2", RegistrationNumber = "test2", CompanySizeId = 2 } },
            new Client() { Id = 3, Name = "Client3", UniqueIdentifier = "guid3", Vat = "test3", Company = company3 }
        };

        mockRepository.Setup(r => r.Get())
            .Returns(clients.AsQueryable);

        return mockRepository;
    }
}
