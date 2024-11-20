// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Queries;

public class ProductModuleStatusDto : IMapFrom<ProductModuleStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductModuleStatus, ProductModuleStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductModuleStatusId));
    }
}
