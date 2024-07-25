using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PropertyRepresentationTypeEnum = FinancialDocument.Domain.Enums.PropertyRepresentationType;

namespace FinancialDocument.Domain.Entities;
public class ProductProperty
{
    [Key]
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int ProductId { get; set; }
    public int PropertyRepresentationId { get; set; }


    [ForeignKey("PropertyId")]
    public Property Property { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    [ForeignKey("PropertyRepresentationId")]
    public PropertyRepresentationType PropertyRepresentationValue { get; set; }
    [NotMapped]
    public PropertyRepresentationTypeEnum PropertyRepresentationType => (PropertyRepresentationTypeEnum) PropertyRepresentationId;
}
