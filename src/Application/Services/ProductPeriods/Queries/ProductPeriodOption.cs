// auto-generated
namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodOption : IMapFrom<ProductPeriod>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPeriod, ProductPeriodOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductPeriodId));
    }
}