using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using FinancialDocument.Persistance.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialDocument.Persistance.Extensions;

public static class DIExtensions
{
    public static void ConfigurePersistance(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        if (!string.IsNullOrWhiteSpace(configuration.GetSection("Database").Value))
        {
        }
        else
        {
            serviceCollection.AddDbContext<FinancialDocumentDbContext>(options => options.UseInMemoryDatabase("Database"));
        }
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<ITenantRepository, TenantRepository>();
        serviceCollection.AddScoped<IClientRepository, ClientRepository>();
        serviceCollection.AddScoped<IDataEntry<ProductProperty>, ProductPropertyRepository>();

        
    }
}
