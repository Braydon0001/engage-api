namespace Engage.Application.Services.EmployeeReinstatementHistories.Models;

public class EmployeeReinstatementHistoryDto : IMapFrom<EmployeeReinstatementHistory>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeReinstatementReasonId { get; set; }
    public string EmployeeReinstatementReasonName { get; set; }
    public DateTime ReinstatementDate { get; set; }
    public string CreatedBy { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeReinstatementHistory, EmployeeReinstatementHistoryDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeReinstatementHistoryId));
    }
}
