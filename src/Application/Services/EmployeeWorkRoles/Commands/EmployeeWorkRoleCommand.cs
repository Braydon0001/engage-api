namespace Engage.Application.Services.EmployeeWorkRoles.Commands;

public class EmployeeWorkRoleCommand : IMapTo<EmployeeWorkRole>
{
    public int ManagerId { get; set; }
    public int EmploymentTypeId { get; set; }
    public int? StatusId { get; set; }
    public int? GradeId { get; set; }
    public int? VacancyId { get; set; }
    public bool IsPromotion { get; set; }
    public bool IsCurrentRole { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ExitReason { get; set; }
    public int GradeLevel { get; set; }
    public string Note { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeWorkRoleCommand, EmployeeWorkRole>();
    }
}
