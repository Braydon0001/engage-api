// auto-generated
using Engage.Application.Services.ProductCategories.Queries;

namespace Engage.Application.Services.ProductSubCategories.Queries;

public class ProductSubCategoryVm : IMapFrom<ProductSubCategory>
{
    public int Id { get; set; }
    public ProductCategoryOption ProductCategoryId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubCategory, ProductSubCategoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSubCategoryId))
               .ForMember(d => d.ProductCategoryId, opt => opt.MapFrom(s => s.ProductCategory));
    }
}
