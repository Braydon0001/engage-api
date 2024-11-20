namespace Engage.Application.Services.Claims.Models;

public class ClaimStoreOptionDto : IMapFrom<Store>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsApproveClaims { get; set; }
    public bool IsEngageRegionClaimManager { get; set; }
    public OptionDto EngageRegionId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Store, ClaimStoreOptionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreId))
            .ForMember(d => d.IsApproveClaims, opt => opt.MapFrom(s => s.EngageRegion.IsApproveClaims))
            .ForMember(d => d.IsEngageRegionClaimManager, opt => opt.MapFrom(s => s.EngageRegion.IsClaimManager))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegionId, s.EngageRegion.Name)));
    }
}
