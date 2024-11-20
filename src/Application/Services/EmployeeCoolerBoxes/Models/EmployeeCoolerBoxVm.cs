namespace Engage.Application.Services.EmployeeCoolerBoxes.Models;

public class EmployeeCoolerBoxVm : IMapFrom<EmployeeCoolerBox>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto EmployeeCoolerBoxConditionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public DateTime? RecievedDate { get; set; }
    public DateTime? HandedBackDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeCoolerBox, EmployeeCoolerBoxVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeCoolerBoxId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, $"{s.Employee.FirstName} {s.Employee.LastName}")))
            .ForMember(d => d.EmployeeCoolerBoxConditionId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeCoolerBoxConditionId, s.EmployeeCoolerBoxCondition.Name)));
    }
}
