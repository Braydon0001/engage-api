namespace Engage.Application.Services.EngageRegionClaimManagers.Models;

public class EngageRegionClaimManagerDto : IMapFrom<EngageRegionClaimManager>
{
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public int ClaimManagerId { get; set; }
    public string ClaimManagerName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageRegionClaimManager, EngageRegionClaimManagerDto>()
             .ForMember(d => d.EngageRegionName, opts => opts.MapFrom(s => s.EngageRegion.Name))
             .ForMember(d => d.ClaimManagerId, opts => opts.MapFrom(s => s.User.UserId))
             .ForMember(d => d.ClaimManagerName, opts => opts.MapFrom(s => s.User.FullName))
             .ForMember(d => d.Email, opts => opts.MapFrom(s => s.User.Email))
             .ForMember(d => d.MobilePhone, opts => opts.MapFrom(s => s.User.MobilePhone));
    }
}
