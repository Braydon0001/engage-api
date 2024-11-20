// auto-generated
namespace Engage.Application.Services.ProductSubCategories.Queries;

public class ProductSubCategoryDto : IMapFrom<ProductSubCategory>
{
    public int Id { get; set; }
    public int ProductCategoryId { get; set; }
    public string ProductCategoryName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubCategory, ProductSubCategoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSubCategoryId));
    }
}
