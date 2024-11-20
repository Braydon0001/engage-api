namespace Engage.Application.Services.EngageRegions.Models;

public class EngageRegionVm : IMapFrom<EngageRegion>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAllRegions { get; set; }
    public bool IsApproveClaims { get; set; }
    public bool IsClaimManager { get; set; }
    public bool Disabled { get; set; }
    public OptionDto StoreSparRegionId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageRegion, EngageRegionVm>()
            .ForMember(d => d.StoreSparRegionId, opt => opt.MapFrom(s => s.StoreSparRegionId.HasValue
                                                                 ? new OptionDto(s.StoreSparRegionId.Value, s.StoreSparRegion.Name)
                                                                 : null));
    }
}
