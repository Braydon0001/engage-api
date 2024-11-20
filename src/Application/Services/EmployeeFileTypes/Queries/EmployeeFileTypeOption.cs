namespace Engage.Application.Services.EmployeeFileTypes.Queries;

public class EmployeeFileTypeOption : IMapFrom<EmployeeFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFileType, EmployeeFileTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeFileTypeId));
    }
}