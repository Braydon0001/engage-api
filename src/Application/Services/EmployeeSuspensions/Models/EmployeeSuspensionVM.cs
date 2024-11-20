namespace Engage.Application.Services.EmployeeSuspensions.Models;

public class EmployeeSuspensionVm : IMapFrom<EmployeeSuspension>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto EmployeeSuspensionReasonId { get; set; }
    public OptionDto InstitutionTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeSuspension, EmployeeSuspensionVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeSuspensionId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.EmployeeSuspensionReasonId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeSuspensionReasonId, s.EmployeeSuspensionReason.Name)));
    }
}
