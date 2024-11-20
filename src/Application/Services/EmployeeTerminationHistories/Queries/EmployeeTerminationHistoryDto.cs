namespace Engage.Application.Services.EmployeeTerminationHistories.Models;

public class EmployeeTerminationHistoryDto : IMapFrom<EmployeeTerminationHistory>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeTerminationReasonId { get; set; }
    public string EmployeeTerminationReasonName { get; set; }
    public DateTime TerminationDate { get; set; }
    public string CreatedBy { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTerminationHistory, EmployeeTerminationHistoryDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTerminationHistoryId));
    }
}
