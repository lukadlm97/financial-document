using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinancialDocument.Infrastructure.Services
{
    public static class DocumentMockery
    {
        public static string GenerateDocument()
        {
            return JsonSerializer.Serialize(new Document("95867648", 42331.12, "EUR", new List<Transaction>()
            {
                new Transaction("2913", 166.95, "1/4/2015", "Grocery shopping", "Food & Dining"),
                new Transaction("3882", 6.58, "24/4/2016", "Grocery shopping", "Food & Dining"),
                new Transaction("1143", -241.07, "1/4/2015", "Gas station purchase", "Utilities"),
            }
            ));
        }
    }
    public record Document(
        [property: JsonPropertyName("account_number")] string AccountNumber,
        [property: JsonPropertyName("balance")] double Balance,
        [property: JsonPropertyName("currency")] string Currency,
        [property: JsonPropertyName("transactions")] IReadOnlyList<Transaction> Transactions
    );

    public record Transaction(
        [property: JsonPropertyName("transaction_id")] string TransactionId,
        [property: JsonPropertyName("amount")] double Amount,
        [property: JsonPropertyName("date")] string Date,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("category")] string Category
    );

}
