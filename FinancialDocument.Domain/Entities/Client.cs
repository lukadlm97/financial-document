using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialDocument.Domain.Entities;
public class Client
{
    [Key]
    public int Id { get; set; }
    public string UniqueIdentifier { get; set; }
    public string Name { get; set; }
    public string VAT { get; set; }
    public int? CompanyId { get; set; }


    [ForeignKey("CompanyId")]
    public Company? Company { get; set; }
}
