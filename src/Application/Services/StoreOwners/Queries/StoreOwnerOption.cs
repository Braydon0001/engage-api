// auto-generated
namespace Engage.Application.Services.StoreOwners.Queries;

public class StoreOwnerOption : IMapFrom<StoreOwner>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreOwner, StoreOwnerOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreOwnerId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}