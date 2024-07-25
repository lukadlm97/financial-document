using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinancialDocument.Persistance
{
    public class FinancialDocumentDbContextFactory : IDesignTimeDbContextFactory<FinancialDocumentDbContext>
    {
        public FinancialDocumentDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FinancialDocumentDbContext>();
            builder.UseInMemoryDatabase("Database");

            return new FinancialDocumentDbContext(builder.Options);
        }
    }
}
