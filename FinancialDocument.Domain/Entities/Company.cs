using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CompanySizeEnum = FinancialDocument.Domain.Enums.CompanySize;

namespace FinancialDocument.Domain.Entities;
public class Company
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string RegistrationNumber { get; set; }
    public int CompanySizeId { get; set; }

    [ForeignKey("CompanySizeId")]
    public CompanySize CompanySizeValue { get; set; }
    [NotMapped]
    public CompanySizeEnum CompanySize => (CompanySizeEnum) CompanySizeId;
}
