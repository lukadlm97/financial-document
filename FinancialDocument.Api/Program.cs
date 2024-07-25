using FinancialDocument.Application.Extensions;
using FinancialDocument.Persistance.Extensions;
using FinancialDocument.Infrastructure.Extensions;
using FinancialDocument.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddHttpLogging(o => { });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterEndpoints();

builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigurePersistance(builder.Configuration);
builder.Services.ConfigureInfrastructure(builder.Configuration);

var app = builder.Build();

app.SeedInMemoryDB();

app.UseHttpLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionMiddleware();

app.UseEndpoints();

app.Run();
