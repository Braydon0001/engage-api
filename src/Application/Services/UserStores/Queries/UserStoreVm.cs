using Engage.Application.Services.Stores.Queries;
using Engage.Application.Services.Users.Queries;

namespace Engage.Application.Services.UserStores.Queries;

public class UserStoreVm : IMapFrom<UserStore>
{
    public int Id { get; init; }
    public UserOption UserId { get; init; }
    public StoreOption StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserStore, UserStoreVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserStoreId))
               .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store));
    }
}
