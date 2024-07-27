using FinancialDocument.Application.Contracts.DTOs;
using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Domain.Entities;

namespace FinancialDocument.Infrastructure.Services;
public class ClientService(IDataRepository<Tenant> _tenantRepository, IDataRepository<Client> _clientRepository) : IClientService
{
    public async Task<ClientMain?> GetAsync(string tenantId, string documentId, CancellationToken cancellationToken)
    {
        var selectedTenant = _tenantRepository.Get()
                                        .FirstOrDefault(x => x.UniqueIdentifier.Equals(tenantId, StringComparison.OrdinalIgnoreCase)
                                                            && x.Document.UniqueIdentifier.Equals(documentId, StringComparison.OrdinalIgnoreCase));
        if (selectedTenant == null)
        {
            return null;
        }

        var selectedClient = _clientRepository.Get()
                                      .FirstOrDefault(x => x.Id == selectedTenant.ClientId); 
        if (selectedClient == null)
        {
            return null;
        }

        return new ClientMain(selectedClient.UniqueIdentifier, selectedClient.Vat);
    }

    public async Task<ClientDetails?> GetDetailsAsync(string VAT, CancellationToken cancellationToken)
    {
        return _clientRepository.Get()
                        .Where(x => x.Vat.Equals(VAT, StringComparison.OrdinalIgnoreCase))
                        .Select(e => new ClientDetails(e.UniqueIdentifier,
                                                        e.Vat,
                                                        e.Company != null ? e.Company.RegistrationNumber : string.Empty,
                                                        e.Company != null ? e.Company.CompanySize.ToString() : string.Empty,
                                                        e.Company != null ? e.Company.CompanySize : null))
                        .FirstOrDefault();
    }

    public async Task<bool> IsWhitelistedAsync(string tenantId, string clientId, CancellationToken cancellationToken)
    {
        var selectedTenant = _tenantRepository.Get()
                                    .FirstOrDefault(x => x.UniqueIdentifier.Equals(tenantId, StringComparison.OrdinalIgnoreCase) 
                                                            && x.Client.UniqueIdentifier.Equals(clientId, StringComparison.OrdinalIgnoreCase));
        if (selectedTenant == null)
        {
            return false;
        }

        return selectedTenant.IsWhitelisted;
    }
}
