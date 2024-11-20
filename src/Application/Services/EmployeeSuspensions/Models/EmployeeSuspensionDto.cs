namespace Engage.Application.Services.EmployeeSuspensions.Models;

public class EmployeeSuspensionDto : IMapFrom<EmployeeSuspension>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeSuspensionReasonId { get; set; }
    public string EmployeeSuspensionReasonName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeSuspension, EmployeeSuspensionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeSuspensionId));
    }
}
