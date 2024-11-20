namespace Engage.Application.Services.ProjectDcProducts.Queries;

public class ProjectDcProductOption : IMapFrom<ProjectDcProduct>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDcProduct, ProjectDcProductOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectDcProductId));
    }
}