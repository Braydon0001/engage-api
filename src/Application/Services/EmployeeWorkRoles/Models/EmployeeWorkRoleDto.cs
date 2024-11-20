namespace Engage.Application.Services.EmployeeWorkRoles.Models;

public class EmployeeWorkRoleDto : IMapFrom<Domain.Entities.EmployeeWorkRole>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int ManagerId { get; set; }
    public string ManagerName { get; set; }
    public int EmploymentTypeId { get; set; }
    public string EmploymentTypeName { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ExitReason { get; set; }
    public int GradeLevel { get; set; }
    public string Note { get; set; }
    public int? StatusId { get; set; }
    public string StatusName { get; set; }
    public int? GradeId { get; set; }
    public string GradeName { get; set; }
    public int? VacancyId { get; set; }
    public string VacancyName { get; set; }
    public bool IsPromotion { get; set; }
    public bool IsCurrentRole { get; set; }

    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.EmployeeWorkRole, EmployeeWorkRoleDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeWorkRoleId))
            .ForMember(d => d.ManagerName, opt => opt.MapFrom(s => s.Manager.FirstName + " " + s.Manager.LastName))
            .ForMember(d => d.EmploymentTypeName, opt => opt.MapFrom(s => s.EmploymentType.Name))
            .ForMember(d => d.StatusName, opt => opt.MapFrom(s => s.Status.Name))
            .ForMember(d => d.GradeName, opt => opt.MapFrom(s => s.Grade.Name))
            .ForMember(d => d.VacancyName, opt => opt.MapFrom(s => s.Vacancy.Name));
    }
}
