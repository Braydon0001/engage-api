// auto-generated
namespace Engage.Domain.Entities;

public class SupplierSubContractDetail : BaseAuditableEntity
{
    public int SupplierSubContractDetailId { get; set; }
    public int SupplierSubContractTypeId { get; set; }
    public int? SupplierSubContractDetailTypeId { get; set; }
    public string Detail { get; set; }
    public string Note { get; set; }

    // Navigation Properties

    public SupplierSubContractType SupplierSubContractType { get; set; }
    public SupplierSubContractDetailType SupplierSubContractDetailType { get; set; }
}