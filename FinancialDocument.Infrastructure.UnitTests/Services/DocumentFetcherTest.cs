using FinancialDocument.Application.Contracts.Services;
using FinancialDocument.Infrastructure.Services;

namespace FinancialDocument.Infrastructure.UnitTests.Services
{
    public class DocumentFetcherTest
    {
        private readonly CancellationToken _token;

        public DocumentFetcherTest()
        {
            _token = CancellationToken.None;
        }
        
        [Theory]
        [InlineData("Product1", "Product1")]
        public async Task Get_DocumentFetched(string code, string documentId)
        {
            IDocumentFetcher documentFetcher = new DocumentFetcher();

            var json = await documentFetcher.Get(code, documentId, _token);

            Assert.NotNull(json);
        }
    }
}
