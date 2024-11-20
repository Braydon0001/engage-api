namespace Engage.Application.Services.EmployeeFiles.Queries;

public class EmployeeFileVm : IMapFrom<EmployeeFile>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto EmployeeFileTypeId { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFile, EmployeeFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeFileId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code)))
               .ForMember(d => d.EmployeeFileTypeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeFileTypeId, s.EmployeeFileType.Name)));
    }
}
