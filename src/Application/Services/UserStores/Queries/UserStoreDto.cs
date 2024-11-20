namespace Engage.Application.Services.UserStores.Queries;

public class UserStoreDto : IMapFrom<UserStore>
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string UserName { get; init; }
    public int StoreId { get; init; }
    public string StoreName { get; init; }
    public string StoreAccountNo { get; set; }
    public string StoreCode { get; set; }
    public string EngageRegionName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserStore, UserStoreDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserStoreId))
               .ForMember(e => e.StoreAccountNo, opt => opt.MapFrom(d => string.Join(",", d.Store.DCAccounts.Select(e => e.AccountNumber))))
               .ForMember(d => d.StoreCode, opt => opt.MapFrom(s => s.Store.Code))
               .ForMember(e => e.EngageRegionName, opt => opt.MapFrom(d => d.Store.EngageRegion.Name));
    }
}
