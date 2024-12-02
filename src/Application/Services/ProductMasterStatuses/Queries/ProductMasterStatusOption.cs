// auto-generated
namespace Engage.Application.Services.ProductMasterStatuses.Queries;

public class ProductMasterStatusOption : IMapFrom<ProductMasterStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterStatus, ProductMasterStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterStatusId));
    }
}