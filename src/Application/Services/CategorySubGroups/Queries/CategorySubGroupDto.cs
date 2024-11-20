namespace Engage.Application.Services.CategorySubGroups.Queries;

public class CategorySubGroupDto : IMapFrom<CategorySubGroup>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategorySubGroup, CategorySubGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategorySubGroupId));
    }
}
