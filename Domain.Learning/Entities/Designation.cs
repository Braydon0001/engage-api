namespace Domain.Learning.Entities;

public class Designation
{
    public int DesignationId { get; set; }
    public int? ApiDesignationId { get; set; }
    public int? AccountId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ExternalCode { get; set; }
}
