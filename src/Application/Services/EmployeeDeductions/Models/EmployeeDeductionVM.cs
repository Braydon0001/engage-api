namespace Engage.Application.Services.EmployeeDeductions.Models;

public class EmployeeDeductionVm : IMapFrom<EmployeeDeduction>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto DeductionCycleTypeId { get; set; }
    public OptionDto DeductionTypeId { get; set; }
    public float Amount { get; set; }
    public DateTime DeductionDate { get; set; }
    public string Note { get; set; }
    public string Reference { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeDeduction, EmployeeDeductionVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(d => d.EmployeeDeductionId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.DeductionCycleTypeId, opt => opt.MapFrom(s => new OptionDto(s.DeductionCycleTypeId, s.DeductionCycleType.Name)))
            .ForMember(d => d.DeductionTypeId, opt => opt.MapFrom(s => new OptionDto(s.DeductionTypeId, s.DeductionType.Name)));
    }
}
