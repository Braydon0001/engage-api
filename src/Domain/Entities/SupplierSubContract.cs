// auto-generated
namespace Engage.Domain.Entities;

public class SupplierSubContract : BaseAuditableEntity
{
    public int SupplierSubContractId { get; set; }
    public int SupplierContractId { get; set; }
    public int SupplierSubContractTypeId { get; set; }
    public string Name { get; set; }
    public string Reference1 { get; set; }
    public string GlMainCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }

    // Navigation Properties

    public SupplierContract SupplierContract { get; set; }
    public SupplierSubContractType SupplierSubContractType { get; set; }
}