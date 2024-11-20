// auto-generated
namespace Engage.Application.Services.ProductMasterSystemStatuses.Queries;

public class ProductMasterSystemStatusDto : IMapFrom<ProductMasterSystemStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSystemStatus, ProductMasterSystemStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterSystemStatusId));
    }
}
