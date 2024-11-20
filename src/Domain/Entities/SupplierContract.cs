// auto-generated
namespace Engage.Domain.Entities;

public class SupplierContract : BaseAuditableEntity
{
    public int SupplierContractId { get; set; }
    public int SupplierId { get; set; }
    public int SupplierContractTypeId { get; set; }
    public int? SupplierContractGroupId { get; set; }
    public int? SupplierContractSubGroupId { get; set; }
    public int SupplierContactId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Vendor { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public string Reference2 { get; set; }
    public List<JsonFile> Files { get; set; }
    public bool IsEngage { get; set; }
    public bool IsEncore { get; set; }
    public bool IsEngine { get; set; }
    public bool IsSpar { get; set; }
    public bool IsTops { get; set; }

    // Navigation Properties

    public Supplier Supplier { get; set; }
    public SupplierContractType SupplierContractType { get; set; }
    public SupplierContractGroup SupplierContractGroup { get; set; }
    public SupplierContractSubGroup SupplierContractSubGroup { get; set; }
    public SupplierContact SupplierContact { get; set; }
}