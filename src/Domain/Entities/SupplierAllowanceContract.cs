namespace Engage.Domain.Entities;

public class SupplierAllowanceContract : BaseAuditableEntity
{
    public SupplierAllowanceContract()
    {
        SupplierAllowanceSubContracts = new HashSet<SupplierAllowanceSubContract>();
    }
    public int SupplierAllowanceContractId { get; set; }
    public int SupplierId { get; set; }
    public int SupplierSalesLeadId { get; set; }
    public string Name { get; set; }
    public string NCircularReference { get; set; }
    public string EncoreReference { get; set; }
    public string Vendor { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Comment { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public SupplierSalesLead SupplierSalesLead { get; set; }
    public ICollection<SupplierAllowanceSubContract> SupplierAllowanceSubContracts { get; set; }
}