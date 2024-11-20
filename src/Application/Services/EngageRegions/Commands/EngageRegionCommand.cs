namespace Engage.Application.Services.EngageRegions.Commands;

public class EngageRegionCommand : IMapTo<EngageRegion>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAllRegions { get; set; }
    public bool IsApproveClaims { get; set; }
    public bool IsClaimManager { get; set; }
    public bool Disabled { get; set; }
    public int? StoreSparRegionId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageRegionCommand, EngageRegion>();
    }
}
