using System.ComponentModel.DataAnnotations;

namespace FinancialDocument.Domain.Entities;
public class Document
{
    [Key]
    public int Id { get; set; }
    public string UniqueIdentifier { get; set; }
    public string Name { get; set; }
}
