// auto-generated
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.StoreOwners.Queries;

public class StoreOwnerVm : IMapFrom<StoreOwner>
{
    public int Id { get; set; }
    public StoreOption StoreId { get; set; }
    public StoreGroup StoreGroupId { get; set; }
    public StoreOwnerType StoreOwnerTypeId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Note { get; set; }
    public string Name { get; set; }
    public bool IsPrimaryOwner { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreOwner, StoreOwnerVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreOwnerId))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store))
               .ForMember(d => d.StoreGroupId, opt => opt.MapFrom(s => s.StoreGroup))
               .ForMember(d => d.StoreOwnerTypeId, opt => opt.MapFrom(s => s.StoreOwnerType));
    }
}
