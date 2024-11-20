namespace Engage.Application.Services.EmployeeKpiTiers.Models;
public class EmployeeKpiTierOption : IMapFrom<EmployeeKpiTier>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpiTier, EmployeeKpiTierOption>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeKpiTierId))
           .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EmployeeKpiId))
           .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}
