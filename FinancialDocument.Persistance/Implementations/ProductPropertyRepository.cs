using FinancialDocument.Application.Contracts.Repositories;
using FinancialDocument.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialDocument.Persistance.Implementations;

public class ProductPropertyRepository (FinancialDocumentDbContext _financialDocumentDbContext) : IDataEntry<ProductProperty>
{
    public IQueryable<ProductProperty> Get()
    {
        return _financialDocumentDbContext.Set<ProductProperty>().Include(x=>x.Property).Include(x=>x.PropertyRepresentationValue);
    }
}
