namespace Engage.Application.Services.ProjectDcProducts.Queries;

public class ProjectDcProductDto : IMapFrom<ProjectDcProduct>
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int DcProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDcProduct, ProjectDcProductDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectDcProductId));
    }
}
