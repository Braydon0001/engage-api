namespace Engage.Application.Services.EmployeeBenefits.Commands;

public class EmployeeBenefitCommand : IMapTo<EmployeeBenefit>
{
    public int BenefitTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Value { get; set; }
    public DateTime IssuedDate { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<EmployeeBenefitCommand, EmployeeBenefit>();
    }
}
