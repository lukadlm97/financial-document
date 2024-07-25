using FinancialDocument.Application.Contracts.DTOs;
using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Application.Contracts.Services;

namespace FinancialDocument.Infrastructure.Services;

public class ClientService(ITenantRepository _tenantRepository, IClientRepository _clientRepository) : IClientService
{
    public async Task<ClientMain?> GetAsync(string tenantId, string documentId, CancellationToken cancellationToken)
    {
        var selectedTenant = (await _tenantRepository.GetAsync(cancellationToken))
                                        .FirstOrDefault(x => x.UniqueIdentifier ==  tenantId 
                                                            && x.Document.UniqueIdentifier == documentId);
        if (selectedTenant == null)
        {
            return null;
        }

        var selectedClient = (await _clientRepository.GetAsync(cancellationToken))
                                                        .FirstOrDefault(x=>x.Id == selectedTenant.ClientId); 
        if (selectedClient == null)
        {
            return null;
        }

        return new ClientMain(selectedClient.UniqueIdentifier, selectedClient.VAT);
    }

    public async Task<ClientDetails?> GetDetailsAsync(string VAT, CancellationToken cancellationToken)
    {
        var selectedClient = (await _clientRepository.GetAsync(cancellationToken)).FirstOrDefault(x=>x.VAT.Equals(VAT, StringComparison.OrdinalIgnoreCase));
        if (selectedClient == null || selectedClient.Company == null)
        {
            return null;
        }

        return new ClientDetails(selectedClient.UniqueIdentifier, 
                                    selectedClient.VAT, 
                                    selectedClient.Company.RegistrationNumber, 
                                    selectedClient.Company.CompanySize.ToString(), 
                                    selectedClient.Company.CompanySize);
    }

    public async Task<bool> IsWhitelistedAsync(string tenantId, string clientId, CancellationToken cancellationToken)
    {
        var selectedTenant = (await _tenantRepository.GetAsync(cancellationToken)).FirstOrDefault(x => x.UniqueIdentifier == tenantId && x.Client.UniqueIdentifier == clientId);
        if (selectedTenant == null)
        {
            return false;
        }

        return selectedTenant.IsWhitelisted;
    }
}
