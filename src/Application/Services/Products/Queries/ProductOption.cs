// auto-generated
namespace Engage.Application.Services.Products.Queries;

public class ProductOption : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name + " - " + s.Code));
    }
}