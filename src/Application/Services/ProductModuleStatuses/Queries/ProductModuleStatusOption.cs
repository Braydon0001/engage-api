// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Queries;

public class ProductModuleStatusOption : IMapFrom<ProductModuleStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductModuleStatus, ProductModuleStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductModuleStatusId));
    }
}