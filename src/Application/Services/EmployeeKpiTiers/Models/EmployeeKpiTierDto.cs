namespace Engage.Application.Services.EmployeeKpiTiers.Models;
public class EmployeeKpiTierDto : IMapFrom<EmployeeKpiTier>
{
    public int Id { get; set; }
    public int EmployeeKpiId { get; set; }
    public string EmployeeKpiName { get; set; }
    public string Name { get; set; }
    public int No { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public List<EmployeeEmployeeKpi> EmployeeKpis { get; set; }
    public List<EmployeeStoreKpi> EmployeeStoreKpis { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpiTier, EmployeeKpiTierDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeKpiTierId))
           .ForMember(d => d.EmployeeKpis, opt => opt.MapFrom(s => s.EmployeeKpis.Select(o =>
                                                                            new EmployeeEmployeeKpi(o.EmployeeId, o.EmployeeKpiId, o.Score, o.EmployeeKpiTierId))))
           .ForMember(d => d.EmployeeStoreKpis, opt => opt.MapFrom(s => s.EmployeeStoreKpis.Select(o =>
                                                                            new EmployeeStoreKpi(o.EmployeeId, o.EmployeeKpiId, o.StoreId, o.EmployeeKpiTierId))));
    }
}
