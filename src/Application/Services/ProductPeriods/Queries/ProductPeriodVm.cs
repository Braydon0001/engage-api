// auto-generated
using Engage.Application.Services.ProductYears.Queries;

namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodVm : IMapFrom<ProductPeriod>
{
    public int Id { get; set; }
    public ProductYearOption ProductYearId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPeriod, ProductPeriodVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductPeriodId))
               .ForMember(d => d.ProductYearId, opt => opt.MapFrom(s => s.ProductYear));
    }
}
