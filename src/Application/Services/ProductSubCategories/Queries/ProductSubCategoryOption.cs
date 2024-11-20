// auto-generated
namespace Engage.Application.Services.ProductSubCategories.Queries;

public class ProductSubCategoryOption : IMapFrom<ProductSubCategory>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSubCategory, ProductSubCategoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSubCategoryId));
    }
}