// auto-generated
namespace Engage.Application.Services.ProductMasterSystemStatuses.Queries;

public class ProductMasterSystemStatusOption : IMapFrom<ProductMasterSystemStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterSystemStatus, ProductMasterSystemStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterSystemStatusId));
    }
}