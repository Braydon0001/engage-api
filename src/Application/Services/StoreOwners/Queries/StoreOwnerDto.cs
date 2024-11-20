// auto-generated
namespace Engage.Application.Services.StoreOwners.Queries;

public class StoreOwnerDto : IMapFrom<StoreOwner>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int StoreGroupId { get; set; }
    public string StoreGroupName { get; set; }
    public string StoreOwnerTypeName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Note { get; set; }
    public string Name { get; set; }
    public bool IsPrimaryOwner { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreOwner, StoreOwnerDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreOwnerId));
    }
}
