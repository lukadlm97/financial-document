using System.ComponentModel.DataAnnotations;

namespace FinancialDocument.Domain.Entities;
public class Property
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
