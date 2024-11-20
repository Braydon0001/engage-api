namespace Engage.Application.Services.CategoryStoreGroups.Queries;

public class CategoryStoreGroupDto : IMapFrom<CategoryStoreGroup>
{
    public int Id { get; init; }
    public int CategoryGroupId { get; init; }
    public string CategoryGroupName { get; init; }
    public int StoreId { get; init; }
    public string StoreName { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryStoreGroup, CategoryStoreGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryStoreGroupId));
    }
}
