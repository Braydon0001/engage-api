
namespace Engage.Application.Services.CategoryGroups.Queries;

public class CategoryGroupVm : IMapFrom<CategoryGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryGroup, CategoryGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryGroupId));
    }
}
