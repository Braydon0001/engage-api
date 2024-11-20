namespace Engage.Application.Services.EmployeeBenefits.Models;

public class EmployeeBenefitVm : IMapFrom<EmployeeBenefit>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto BenefitTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Value { get; set; }
    public DateTime IssuedDate { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeBenefit, EmployeeBenefitVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeBenefitId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.BenefitTypeId, opt => opt.MapFrom(s => new OptionDto(s.BenefitTypeId, s.BenefitType.Name)));
    }
}
