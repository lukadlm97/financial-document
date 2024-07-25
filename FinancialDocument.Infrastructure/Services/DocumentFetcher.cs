using FinancialDocument.Application.Contracts.Services;

namespace FinancialDocument.Infrastructure.Services;

public class DocumentFetcher : IDocumentFetcher
{
    public async Task<string> Get(string tenantId, string documentId, CancellationToken cancellationToken = default)
    {
        // simulate api call
        await Task.Delay(TimeSpan.FromSeconds(1));
        return DocumentMockery.GenerateDocument();
    }
}
