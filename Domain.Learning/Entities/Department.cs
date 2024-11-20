namespace Domain.Learning.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public int? ApiDepartmentId { get; set; }
    public int? AccountId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ExternalCode { get; set; }
}
