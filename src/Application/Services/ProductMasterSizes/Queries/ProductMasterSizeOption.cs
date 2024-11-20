// auto-generated
namespace Engage.Application.Services.ProductMasterSizes.Queries;

public class ProductMasterSizeOption : IMapFrom<ProductMasterSize>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSize, ProductMasterSizeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterSizeId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}