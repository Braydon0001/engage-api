// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Queries;

public class ProductModuleStatusVm : IMapFrom<ProductModuleStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductModuleStatus, ProductModuleStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductModuleStatusId));
    }
}
