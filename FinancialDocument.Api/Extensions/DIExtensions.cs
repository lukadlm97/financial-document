using FinancialDocument.Domain.Exceptions;
using FinancialDocument.Persistance;
using FluentValidation;
using System.Reflection;

namespace FinancialDocument.Api.Extensions;
public static class DIExtensions
{
    public static void RegisterEndpoints(this IServiceCollection services)
    {
        var endpointTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => typeof(IEndpoint).IsAssignableFrom(type) && !type.IsInterface);

        foreach (var type in endpointTypes)
        {
            try
            {
                services.AddScoped(typeof(IEndpoint), type);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public static void UseEndpoints(this WebApplication app)
    {
        IServiceScope scope = app.Services.CreateScope();
        var endpoints = scope.ServiceProvider.GetServices<IEndpoint>();

        foreach (var instance in endpoints)
        {
            try
            {
                instance.MapEndpoints(app);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public static void SeedInMemoryDB(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<FinancialDocumentDbContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                if (app.Logger.IsEnabled(LogLevel.Error))
                {
                    app.Logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
                }
            }
        }
    }

    public static void UseExceptionMiddleware(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                if (ex is DocumentNotFoundException
                    || ex is ClientDetailsNotFoundException)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(ex.Message);
                }
            }
        });
    }
}
