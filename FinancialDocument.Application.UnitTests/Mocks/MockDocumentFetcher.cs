using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Infrastructure.Services;
using Moq;
using System.Text.Json;

namespace FinancialDocument.Application.UnitTests.Mocks;

internal class MockDocumentFetcher
{
    public static Mock<IDocumentFetcher> GetDocumentFetcher()
    {
        var mockRepository = new Mock<IDocumentFetcher>();


        mockRepository.Setup(r => r.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string code, string te, CancellationToken cancellationToken) =>
            { return JsonSerializer.Serialize(new Document(Guid.NewGuid().ToString(), new Random().NextDouble(), "EUR", new List<Transaction>()
        {
            new Transaction(Guid.NewGuid().ToString(), new Random().NextDouble(), "2024-01-01", "description", "category"),
        }
        )); ;
            });

        return mockRepository;
    }
}
