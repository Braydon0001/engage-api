// auto-generated
using Engage.Application.Services.ProductSubGroups.Queries;

namespace Engage.Application.Services.ProductCategories.Queries;

public class ProductCategoryVm : IMapFrom<ProductCategory>
{
    public int Id { get; set; }
    public ProductSubGroupOption ProductSubGroupId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductCategory, ProductCategoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductCategoryId))
               .ForMember(d => d.ProductSubGroupId, opt => opt.MapFrom(s => s.ProductSubGroup));
    }
}
