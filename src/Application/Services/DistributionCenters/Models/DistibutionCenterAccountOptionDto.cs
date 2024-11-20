namespace Engage.Application.Services.DistributionCenters.Models;

public class DistibutionCenterAccountOptionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }
    public bool Disabled { get; set; }
    public int AccountId { get; set; }
    public bool IsPrimary { get; set; }
}
