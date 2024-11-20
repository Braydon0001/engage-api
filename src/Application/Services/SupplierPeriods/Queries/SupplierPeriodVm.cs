using Engage.Application.Services.SupplierYears.Queries;

namespace Engage.Application.Services.SupplierPeriods.Queries;

public class SupplierPeriodVm : IMapFrom<SupplierPeriod>
{
    public int Id { get; set; }
    public SupplierYearOption SupplierYearId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierPeriod, SupplierPeriodVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierPeriodId))
            .ForMember(d => d.SupplierYearId, opt => opt.MapFrom(s => s.SupplierYear));
    }

}
