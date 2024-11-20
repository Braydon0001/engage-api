namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Models;

public class EmployeeDisciplinaryProcedureVm : IMapFrom<EmployeeDisciplinaryProcedure>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public string Description { get; set; }
    public DateTime DisciplinaryProcedureDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeDisciplinaryProcedure, EmployeeDisciplinaryProcedureVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeDisciplinaryProcedureId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)));
    }
}
