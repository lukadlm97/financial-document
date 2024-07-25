using System.ComponentModel.DataAnnotations;

namespace FinancialDocument.Domain.Entities;
public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsEnabled { get; set; }
}
