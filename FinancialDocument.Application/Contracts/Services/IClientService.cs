using FinancialDocument.Application.Contracts.DTOs;

namespace FinancialDocument.Application.Contracts.Services;
public interface IClientService
{
    Task<ClientMain?> GetAsync(string tenantId, string documentId, CancellationToken cancellationToken);
    Task<bool> IsWhitelistedAsync(string tenantId, string id, CancellationToken cancellationToken);
    Task<ClientDetails?> GetDetailsAsync(string VAT, CancellationToken cancellationToken);
}
