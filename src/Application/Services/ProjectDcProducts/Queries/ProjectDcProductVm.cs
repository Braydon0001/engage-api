
using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectDcProducts.Queries;

public class ProjectDcProductVm : IMapFrom<ProjectDcProduct>
{
    public int Id { get; init; }
    public ProjectOption ProjectId { get; init; }
    public OptionDto DcProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDcProduct, ProjectDcProductVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectDcProductId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Project))
               .ForMember(d => d.DcProductId, opt => opt.MapFrom(s => new OptionDto { Id = s.DcProductId, Name = s.DcProduct.Name }));
    }
}
