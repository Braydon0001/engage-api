namespace Domain.Learning.Entities;

public class Region
{
    public int RegionId { get; set; }
    public int? ApiRegionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ExternalCode { get; set; }
}
