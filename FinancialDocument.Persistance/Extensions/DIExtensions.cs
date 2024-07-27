using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using FinancialDocument.Persistance.Implementations;

namespace FinancialDocument.Persistance.Extensions;

public static class DIExtensions
{
    public static void ConfigurePersistance(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        if (!string.IsNullOrWhiteSpace(configuration.GetConnectionString("Database")))
        {
        }
        else
        {
            serviceCollection.AddDbContext<FinancialDocumentDbContext>(options => options.UseInMemoryDatabase("Database"));
        }
        serviceCollection.AddScoped<IDataRepository<Product>, DataRepository<Product>>();
        serviceCollection.AddScoped<IDataRepository<Tenant>, DataRepository<Tenant>>();
        serviceCollection.AddScoped<IDataRepository<Client>, DataRepository<Client>>();
        serviceCollection.AddScoped<IDataRepository<ProductProperty>, DataRepository<ProductProperty>>();

        
    }
}
