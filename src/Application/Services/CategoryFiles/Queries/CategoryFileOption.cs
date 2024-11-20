namespace Engage.Application.Services.CategoryFiles.Queries;

public class CategoryFileOption : IMapFrom<CategoryFile>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFile, CategoryFileOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryFileId));
    }
}