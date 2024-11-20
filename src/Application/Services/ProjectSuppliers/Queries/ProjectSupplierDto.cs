namespace Engage.Application.Services.ProjectSuppliers.Queries;

public class ProjectSupplierDto : IMapFrom<ProjectSupplier>
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int SupplierId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSupplier, ProjectSupplierDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSupplierId));
    }
}
