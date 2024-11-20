namespace Engage.Domain.Entities.FileEntities;

public class EmployeeBankDetailFile : BaseFile
{
    public int EmployeeBankDetailFileId { get; set; }
    public int EmployeeBankDetailId { get; set; }

    // Navigation Properties
    public EmployeeBankDetail EmployeeBankDetail { get; set; }
}
