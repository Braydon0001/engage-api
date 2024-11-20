// auto-generated
namespace Engage.Application.Services.ProductMasterColors.Queries;

public class ProductMasterColorOption : IMapFrom<ProductMasterColor>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterColor, ProductMasterColorOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterColorId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}