namespace Engage.Application.Services.SupplierPeriods.Queries;

public class SupplierPeriodOption : IMapFrom<SupplierPeriod>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierPeriod, SupplierPeriodOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierPeriodId));
    }
}
