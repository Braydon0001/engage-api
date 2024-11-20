// auto-generated
namespace Engage.Application.Services.ProductCategories.Queries;

public class ProductCategoryDto : IMapFrom<ProductCategory>
{
    public int Id { get; set; }
    public int ProductSubGroupId { get; set; }
    public string ProductSubGroupName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCategory, ProductCategoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductCategoryId));
    }
}
