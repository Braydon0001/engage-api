// auto-generated
namespace Engage.Application.Services.ProductCategories.Queries;

public class ProductCategoryOption : IMapFrom<ProductCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCategory, ProductCategoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductCategoryId));
    }
}