namespace Engage.Application.Services.CategoryTargetTypes.Queries;

public class CategoryTargetTypeOption : IMapFrom<CategoryTargetType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetType, CategoryTargetTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTargetTypeId));
    }
}