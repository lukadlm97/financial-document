namespace FinancialDocument.Application.Contracts.Services;
public interface IDocumentFetcher
{
    Task<string> Get(string tenantId, string documentId, CancellationToken cancellationToken = default);
}
