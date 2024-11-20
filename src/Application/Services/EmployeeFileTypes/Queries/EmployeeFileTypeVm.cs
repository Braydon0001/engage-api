namespace Engage.Application.Services.EmployeeFileTypes.Queries;

public class EmployeeFileTypeVm : IMapFrom<EmployeeFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFileType, EmployeeFileTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeFileTypeId));
    }
}
