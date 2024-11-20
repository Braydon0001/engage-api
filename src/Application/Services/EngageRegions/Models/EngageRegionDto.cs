namespace Engage.Application.Services.EngageRegions.Models;

public class EngageRegionDto : IMapFrom<EngageRegion>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? StoreSparRegionId { get; set; }
    public string StoreSparRegionName { get; set; }
    public string Description { get; set; }
    public bool IsAllRegions { get; set; }
    public bool IsApproveClaims { get; set; }
    public bool IsClaimManager { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageRegion, EngageRegionDto>();
    }
}
