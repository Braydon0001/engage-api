namespace Engage.Application.Services.CategoryFileTypes.Queries;

public class CategoryFileTypeDto : IMapFrom<CategoryFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFileType, CategoryFileTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryFileTypeId));
    }
}
