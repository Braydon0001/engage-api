namespace Engage.Application.Services.EmployeeCoolerBoxes.Models;

public class EmployeeCoolerBoxDto : IMapFrom<EmployeeCoolerBox>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public DateTime? EmployeeEndDate { get; set; }
    public int EmployeeCoolerBoxConditionId { get; set; }
    public string EmployeeCoolerBoxConditionName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public DateTime? RecievedDate { get; set; }
    public DateTime? HandedBackDate { get; set; }
    public List<EmployeeCoolerBoxHistoryDto> EmployeeCoolerBoxHistories { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeCoolerBox, EmployeeCoolerBoxDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeCoolerBoxId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName}"))
            .ForMember(d => d.EmployeeCoolerBoxHistories,
                    opt => opt.MapFrom(s => s.EmployeeCoolerBoxHistories
                        .OrderByDescending(e => e.EmployeeCoolerBoxHistoryId)
                        .Select(e => new EmployeeCoolerBoxHistoryDto()
                        {
                            Id = e.EmployeeCoolerBoxHistoryId,
                            EmployeeCode = e.OldEmployee.Code,
                            EmployeeId = e.OldEmployeeId,
                            EmployeeName = $"{e.OldEmployee.FirstName} {e.OldEmployee.LastName}",
                            EmployeeCoolerBoxId = e.EmployeeCoolerBoxId,
                            CreatedDate = e.Created,
                        })
                        .ToList()));
    }
}

public class EmployeeCoolerBoxHistoryDto
{
    public int Id { get; set; }
    public int EmployeeCoolerBoxId { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public DateTime? CreatedDate { get; set; }
}
