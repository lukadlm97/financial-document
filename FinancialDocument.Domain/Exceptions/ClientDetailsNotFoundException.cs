namespace FinancialDocument.Domain.Exceptions;

public class ClientDetailsNotFoundException : Exception
{
    public ClientDetailsNotFoundException(string message) : base(message) { }
}
