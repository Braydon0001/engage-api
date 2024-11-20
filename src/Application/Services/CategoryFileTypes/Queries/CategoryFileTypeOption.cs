namespace Engage.Application.Services.CategoryFileTypes.Queries;

public class CategoryFileTypeOption : IMapFrom<CategoryFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFileType, CategoryFileTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryFileTypeId));
    }
}