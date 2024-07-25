using System.ComponentModel.DataAnnotations;

namespace FinancialDocument.Domain.Entities;
public class PropertyRepresentationType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
