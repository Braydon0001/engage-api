// auto-generated
namespace Engage.Application.Services.ProductPackSizeTypes.Queries;

public class ProductPackSizeTypeDto : IMapFrom<ProductPackSizeType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductPackSizeType, ProductPackSizeTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductPackSizeTypeId));
    }
}
