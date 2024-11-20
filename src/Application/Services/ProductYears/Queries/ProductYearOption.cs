// auto-generated
namespace Engage.Application.Services.ProductYears.Queries;

public class ProductYearOption : IMapFrom<ProductYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductYear, ProductYearOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductYearId));
    }
}