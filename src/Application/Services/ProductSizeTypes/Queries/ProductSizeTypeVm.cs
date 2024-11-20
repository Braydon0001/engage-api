// auto-generated
namespace Engage.Application.Services.ProductSizeTypes.Queries;

public class ProductSizeTypeVm : IMapFrom<ProductSizeType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSizeType, ProductSizeTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSizeTypeId));
    }
}
