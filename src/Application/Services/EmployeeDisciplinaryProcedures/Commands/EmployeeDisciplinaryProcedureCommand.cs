namespace Engage.Application.Services.EmployeeDisciplinaryProcedures.Commands;

public class EmployeeDisciplinaryProcedureCommand : IMapTo<EmployeeDisciplinaryProcedure>
{
    public int EmployeeId { get; set; }
    public string Description { get; set; }
    public DateTime DisciplinaryProcedureDate { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeDisciplinaryProcedureCommand, EmployeeDisciplinaryProcedure>();
    }
}
