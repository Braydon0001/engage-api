namespace Engage.Domain.Entities.FileEntities;

public class EmployeeCoolerBoxFile : BaseFile
{
    public int EmployeeCoolerBoxFileId { get; set; }
    public int EmployeeCoolerBoxId { get; set; }

    // Navigation Properties
    public EmployeeCoolerBox EmployeeCoolerBox { get; set; }

}
