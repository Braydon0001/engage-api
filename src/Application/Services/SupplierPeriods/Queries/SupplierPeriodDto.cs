namespace Engage.Application.Services.SupplierPeriods.Queries;

public class SupplierPeriodDto : IMapFrom<SupplierPeriod>
{
    public int Id { get; set; }
    public int SupplierYearId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierPeriod, SupplierPeriodDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierPeriodId));
    }
}
