// auto-generated
namespace Engage.Application.Services.ProductSizeTypes.Queries;

public class ProductSizeTypeDto : IMapFrom<ProductSizeType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSizeType, ProductSizeTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSizeTypeId));
    }
}
