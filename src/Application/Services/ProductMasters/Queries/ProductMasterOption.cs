// auto-generated
namespace Engage.Application.Services.ProductMasters.Queries;

public class ProductMasterOption : IMapFrom<ProductMaster>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMaster, ProductMasterOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name + " - " + s.Code));
    }
}