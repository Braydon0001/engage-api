namespace Engage.Application.Services.Stores.Queries;

public class StoreOption : BaseOption, IMapFrom<Store>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Store, StoreOption>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreId));
    }
}
