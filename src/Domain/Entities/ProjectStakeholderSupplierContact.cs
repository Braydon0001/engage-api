namespace Engage.Domain.Entities;

public class ProjectStakeholderSupplierContact : ProjectStakeholder
{
    public int SupplierContactId { get; set; }
    public SupplierContact SupplierContact { get; set; }
}
