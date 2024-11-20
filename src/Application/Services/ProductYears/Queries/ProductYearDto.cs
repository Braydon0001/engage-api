// auto-generated
namespace Engage.Application.Services.ProductYears.Queries;

public class ProductYearDto : IMapFrom<ProductYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductYear, ProductYearDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductYearId));
    }
}
