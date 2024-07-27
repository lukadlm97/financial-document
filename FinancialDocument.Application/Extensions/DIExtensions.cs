using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using FluentValidation;

using FinancialDocument.Application.Behaviours;
using FinancialDocument.Application.Configurations;


namespace FinancialDocument.Application.Extensions;

public static class DIExtensions
{
    public static void ConfigureApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(DIExtensions)));

        serviceCollection.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));

            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        serviceCollection.Configure<PerformanceThresholdSettings>(configuration.GetSection(nameof(PerformanceThresholdSettings)));
    }
}
