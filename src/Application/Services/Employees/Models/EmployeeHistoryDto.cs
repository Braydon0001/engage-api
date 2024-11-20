namespace Engage.Application.Services.Employees.Models;

public class EmployeeHistoryDto : IMapFrom<Employee>, IMapFrom<EmployeeTerminationHistory>, IMapFrom<EmployeeReinstatementHistory>
{
    public int Id { get; set; }
    public string Operation { get; set; }
    public string Reason { get; set; }
    public DateTime Date { get; set; }
    public string ActionedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTerminationHistory, EmployeeHistoryDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTerminationHistoryId))
            .ForMember(d => d.Reason, opt => opt.MapFrom(s => s.EmployeeTerminationReason.Name))
            .ForMember(d => d.Date, opt => opt.MapFrom(s => s.TerminationDate))
            .ForMember(d => d.ActionedBy, opt => opt.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.Operation, opt => opt.MapFrom(s => "Termination"));

        profile.CreateMap<EmployeeReinstatementHistory, EmployeeHistoryDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeReinstatementHistoryId))
           .ForMember(d => d.Reason, opt => opt.MapFrom(s => s.EmployeeReinstatementReason.Name))
           .ForMember(d => d.Date, opt => opt.MapFrom(s => s.ReinstatementDate))
           .ForMember(d => d.ActionedBy, opt => opt.MapFrom(s => s.CreatedBy))
           .ForMember(d => d.Operation, opt => opt.MapFrom(s => "Re-Instatement"));
    }
}

