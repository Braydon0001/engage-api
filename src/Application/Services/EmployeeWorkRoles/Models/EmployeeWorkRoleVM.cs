namespace Engage.Application.Services.EmployeeWorkRoles.Models;

public class EmployeeWorkRoleVm : IMapFrom<EmployeeWorkRole>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto ManagerId { get; set; }
    public OptionDto EmploymentTypeId { get; set; }
    public OptionDto StatusId { get; set; }
    public OptionDto GradeId { get; set; }
    public OptionDto VacancyId { get; set; }
    public bool IsPromotion { get; set; }
    public bool IsCurrentRole { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ExitReason { get; set; }
    public string Note { get; set; }
    public int GradeLevel { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeWorkRole, EmployeeWorkRoleVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeWorkRoleId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.ManagerId, opt => opt.MapFrom(s => new OptionDto(s.ManagerId, s.Manager.FirstName + " " + s.Manager.LastName)))
            .ForMember(d => d.EmploymentTypeId, opt => opt.MapFrom(s => new OptionDto(s.EmploymentTypeId, s.EmploymentType.Name)))
            .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.StatusId.HasValue
                                                                          ? new OptionDto(s.StatusId.Value, s.Status.Name)
                                                                          : null))
            .ForMember(d => d.GradeId, opt => opt.MapFrom(s => s.GradeId.HasValue
                                                                          ? new OptionDto(s.GradeId.Value, s.Grade.Name)
                                                                          : null))
            .ForMember(d => d.VacancyId, opt => opt.MapFrom(s => s.VacancyId.HasValue
                                                                          ? new OptionDto(s.VacancyId.Value, s.Vacancy.Name)
                                                                          : null));
    }
}
