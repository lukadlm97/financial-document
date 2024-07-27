using FinancialDocument.Domain.Entities;

namespace FinancialDocument.Persistance
{
    public static class DbInitializer
    {
        public static void Initialize(FinancialDocumentDbContext context)
        {
            // Look for any products.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var product1 = new Product() { Id = 1, Code = "ProductA", Name = "Product1", IsEnabled = true };
            var product2 = new Product() { Id = 2, Code = "ProductB", Name = "Product2", IsEnabled = true };
            var product3 = new Product() { Id = 3, Code = "ProductC", Name = "Product3", IsEnabled = false };
            var products = new List<Product>()
            {
                product1,
                product2,
                product3
            };

            var document1 = new Document() { Id = 1, Name = "Document1", UniqueIdentifier = "guid" };
            var company3 = new Company() { Id = 3, Name = "Company3", RegistrationNumber = "test3", CompanySizeId = 3 };

            var tenants = new List<Tenant>()
            {
                new Tenant() 
                { 
                    Id = 1, 
                    ClientId = 1, 
                    DocumentId = 1, 
                    IsWhitelisted = true, 
                    UniqueIdentifier = "guid", 
                    Client =  new Client() { Id = 1, Name = "Client1", UniqueIdentifier = "guid", Vat = "test1", 
                    Company = new Company() { Id = 1, Name = "Company1", RegistrationNumber = "test1", CompanySizeId = 2 }},
                    Document =  document1,
                },
                new Tenant() 
                { 
                    Id = 2, 
                    ClientId = 2, 
                    DocumentId = 2, 
                    IsWhitelisted = true, 
                    UniqueIdentifier = "guid2", 
                    Client=  new Client() { Id = 2, Name = "Client2", UniqueIdentifier = "guid2", Vat = "test2",
                    Company = new Company() { Id = 2, Name = "Company2", RegistrationNumber = "test2", CompanySizeId = 1 } },
                    Document =document1
                },
                new Tenant() 
                { 
                    Id = 3, 
                    ClientId = 3, 
                    DocumentId = 3, 
                    IsWhitelisted = true, 
                    UniqueIdentifier = "guid3", 
                    Client =  new Client() { Id = 3, Name = "Client3", UniqueIdentifier = "guid3", Vat = "test3", Company = company3 },
                    Document = new Document() { Id = 2, Name = "Document2", UniqueIdentifier = "guid2" }
                },
                new Tenant() 
                { 
                    Id = 4, 
                    ClientId = 1, 
                    DocumentId = 1, 
                    IsWhitelisted = false, 
                    UniqueIdentifier = "guid4", 
                    Client =    new Client() { Id = 4, Name = "Client4", UniqueIdentifier = "guid4", Vat = "test4", Company = company3 }, 
                    Document = new Document() { Id = 3, Name = "Document3", UniqueIdentifier = "guid3" }
                }
            };

            var property2 = new PropertyRepresentationType()
            {
                Id = 2,
                Name = "Hash"
            };
            var property3 = new PropertyRepresentationType()
            {
                Id = 3,
                Name = "Unchanged"
            };
            var productProperties = new List<ProductProperty>()
            {
                new ProductProperty()
                {
                        Id = 1,
                        Product = product1,
                        Property =  new Property(){ Id = 1, Name = "account_number"},
                        PropertyRepresentationValue = property2,
                        PropertyRepresentationId = 2
                },
                new ProductProperty()
                {
                        Id = 2,
                        Product = product1,
                        Property =  new Property(){ Id = 2, Name = "balance"},
                        PropertyRepresentationValue = property3,
                        PropertyRepresentationId = 3
                },     
                new ProductProperty()
                {
                        Id = 3,
                        Product = product1,
                        Property =  new Property(){ Id = 3, Name = "transaction_id" },
                        PropertyRepresentationValue =  property2,
                        PropertyRepresentationId = 3
                },
                new ProductProperty()
                {
                        Id = 5,
                        Product = product1,
                        Property =  new Property(){ Id = 5, Name = "category" },
                        PropertyRepresentationValue = property3,
                        PropertyRepresentationId = 2
                },
                new ProductProperty()
                {
                        Id = 6,
                        Product = product1,
                        Property =  new Property(){ Id = 6, Name = "currency"},
                        PropertyRepresentationValue = property3,
                        PropertyRepresentationId = 3
                },
                new ProductProperty()
                {
                        Id = 7,
                        Product = product1,
                        Property =  new Property(){ Id = 7, Name = "amount"},
                        PropertyRepresentationValue = property3,
                        PropertyRepresentationId = 3
                },
                new ProductProperty()
                {
                        Id = 8,
                        Product = product1,
                        Property =  new Property(){ Id = 8, Name = "date"},
                        PropertyRepresentationValue = property3,
                        PropertyRepresentationId = 3
                },
            };

            context.Products.AddRange(products);
            context.Tenants.AddRange(tenants);
            context.ProductProperties.AddRange(productProperties);
            context.SaveChanges();
        }
    }
}
