// auto-generated
namespace Engage.Domain.Entities;

public class SupplierContractAmount : BaseAuditableEntity
{
    public int SupplierContractAmountId { get; set; }
    public int SupplierSubContractDetailId { get; set; }
    public int SupplierContractAmountTypeId { get; set; }
    public int? SupplierContractSplitId { get; set; }
    public float Amount { get; set; }
    public float? StartRangeAmount { get; set; }
    public float? EndRangeAmount { get; set; }
    public bool IsAmountPercent { get; set; }
    public bool IsRangeAmountPercent { get; set; }
    public List<JsonText> Note { get; set; }

    // Navigation Properties

    public SupplierSubContractDetail SupplierSubContractDetail { get; set; }
    public SupplierContractAmountType SupplierContractAmountType { get; set; }
    public SupplierContractSplit SupplierContractSplit { get; set; }
}