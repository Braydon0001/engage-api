namespace Engage.Application.Services.EmployeeKpis.Models;
public class EmployeeKpiDto : IMapFrom<EmployeeKpi>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int EmployeeKpiTypeId { get; set; }
    public string EmployeeKpiTypeName { get; set; }
    public List<EmployeeEmployeeKpi> EmployeeKpis { get; set; }
    public List<EmployeeStoreKpi> EmployeeStoreKpis { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpi, EmployeeKpiDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeKpiId))
           .ForMember(d => d.EmployeeKpis, opt => opt.MapFrom(s => s.EmployeeKpis.Select(o =>
                                                                            new EmployeeEmployeeKpi(o.EmployeeId, o.EmployeeKpiId, o.Score, o.EmployeeKpiTierId))))
           .ForMember(d => d.EmployeeStoreKpis, opt => opt.MapFrom(s => s.EmployeeStoreKpis.Select(o =>
                                                                            new EmployeeStoreKpi(o.EmployeeId, o.EmployeeKpiId, o.StoreId, o.EmployeeKpiTierId))));
    }
}
