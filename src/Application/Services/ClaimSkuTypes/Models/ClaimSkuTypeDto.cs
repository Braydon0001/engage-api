namespace Engage.Application.Services.ClaimSkuTypes.Models;

public class ClaimSkuTypeDto : IMapFrom<ClaimSkuType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimSkuType, ClaimSkuTypeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimSkuTypeId));
    }
}
