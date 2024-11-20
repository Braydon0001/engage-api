namespace Engage.Domain.Entities.FileEntities;

public class EmployeeQualificationFile : BaseFile
{
    public int EmployeeQualificationFileId { get; set; }
    public int EmployeeQualificationId { get; set; }

    // Navigation Properties
    public EmployeeQualification EmployeeQualification { get; set; }

}
