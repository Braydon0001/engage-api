namespace Engage.Application.Services.CategoryGroups.Queries;

public class CategoryGroupOption : IMapFrom<CategoryGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryGroup, CategoryGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryGroupId));
    }
}