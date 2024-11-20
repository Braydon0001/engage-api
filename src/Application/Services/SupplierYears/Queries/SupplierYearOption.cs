namespace Engage.Application.Services.SupplierYears.Queries;

public class SupplierYearOption : IMapFrom<SupplierYear>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierYear, SupplierYearOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierYearId));
    }

}
