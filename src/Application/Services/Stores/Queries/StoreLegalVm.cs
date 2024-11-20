namespace Engage.Application.Services.Stores.Queries;

public class StoreLegalVm : IMapFrom<Store>
{
    public int Id { get; set; }
    public string VatNumber { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Store, StoreLegalVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreId));
    }
}
