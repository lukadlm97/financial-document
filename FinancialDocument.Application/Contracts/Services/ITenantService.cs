namespace FinancialDocument.Application.Contracts.Services;
public interface ITenantService
{
    Task<bool> IsWhitelistedAsync(string tenantId, CancellationToken cancellationToken = default);
}
