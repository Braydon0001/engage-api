// auto-generated
namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodDto : IMapFrom<ProductPeriod>
{
    public int Id { get; set; }
    public int ProductYearId { get; set; }
    public string ProductYearName { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPeriod, ProductPeriodDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductPeriodId));
    }
}
