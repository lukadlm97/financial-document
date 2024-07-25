using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialDocument.Infrastructure.Extensions;

public static class DIExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IProductService, ProductService>();
        serviceCollection.AddScoped<ITenantService, TenantService>();
        serviceCollection.AddScoped<IClientService, ClientService>();
        serviceCollection.AddScoped<IDocumentFetcher, DocumentFetcher>();
    }
}
