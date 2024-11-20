namespace Engage.Application.Services.CategorySubGroups.Queries;

public class CategorySubGroupOption : IMapFrom<CategorySubGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategorySubGroup, CategorySubGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategorySubGroupId));
    }
}