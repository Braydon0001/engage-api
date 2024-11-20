namespace Engage.Application.Services.EmployeeSuspensions.Commands;

public class EmployeeSuspensionCommand : IMapTo<EmployeeSuspension>
{
    public int EmployeeSuspensionReasonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeSuspensionCommand, EmployeeSuspension>();
    }
}
