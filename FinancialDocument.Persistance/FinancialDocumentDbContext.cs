using FinancialDocument.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocument.Persistance;

public class FinancialDocumentDbContext : DbContext
{
    public FinancialDocumentDbContext(DbContextOptions<FinancialDocumentDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<ProductProperty> ProductProperties { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancialDocumentDbContext).Assembly);
    }
}
