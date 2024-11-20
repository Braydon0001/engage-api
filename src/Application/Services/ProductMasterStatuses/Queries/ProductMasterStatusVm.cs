// auto-generated
namespace Engage.Application.Services.ProductMasterStatuses.Queries;

public class ProductMasterStatusVm : IMapFrom<ProductMasterStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductMasterStatus, ProductMasterStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductMasterStatusId));
    }
}
