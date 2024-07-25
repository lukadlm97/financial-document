using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialDocument.Domain.Entities;
public class Tenant
{
    [Key]
    public int Id { get; set; }
    public string UniqueIdentifier { get; set; }
    public int DocumentId { get; set; }
    public int ClientId { get; set; }
    public bool IsWhitelisted { get; set; }

    [ForeignKey("DocumentId")]
    public Document Document { get; set; }
    [ForeignKey("ClientId")]
    public Client Client { get; set; }
}
