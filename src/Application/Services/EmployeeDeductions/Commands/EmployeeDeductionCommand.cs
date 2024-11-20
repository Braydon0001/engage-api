namespace Engage.Application.Services.EmployeeDeductions.Commands;

public class EmployeeDeductionCommand : IMapTo<EmployeeDeduction>
{
    public int DeductionCycleTypeId { get; set; }
    public int DeductionTypeId { get; set; }
    public float Amount { get; set; }
    public DateTime DeductionDate { get; set; }
    public string Note { get; set; }
    public string Reference { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeDeductionCommand, EmployeeDeduction>();
    }
}
