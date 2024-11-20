// auto-generated
namespace Engage.Domain.Entities;

public class SupplierContractSubGroup : BaseAuditableEntity
{
    public int SupplierContractSubGroupId { get; set; }
    public int SupplierContractGroupId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public SupplierContractGroup SupplierContractGroup { get; set; }
}