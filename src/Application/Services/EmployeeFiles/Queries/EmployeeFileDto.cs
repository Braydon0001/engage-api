namespace Engage.Application.Services.EmployeeFiles.Queries;

public class EmployeeFileDto : IMapFrom<EmployeeFile>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeFileTypeId { get; set; }
    public string EmployeeFileTypeName { get; set; }
    public string FileTypeName { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFile, EmployeeFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeFileId))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code))
               .ForMember(d => d.FileTypeName, opt => opt.MapFrom(s => s.EmployeeFileType.Name));
    }
}
