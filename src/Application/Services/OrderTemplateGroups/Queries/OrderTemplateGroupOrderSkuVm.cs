using Engage.Application.Services.OrderSkus.Queries;

namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupOrderSkuVm : OrderTemplateGroupVm, IMapFrom<OrderTemplateGroup>
{
    public List<OrderSkuProductVm> OrderSkus { get; set; }
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateGroup, OrderTemplateGroupOrderSkuVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateGroupId))
               .ForMember(d => d.OrderTemplateProducts, opt => opt.MapFrom(s => s.OrderTemplateProducts));
    }
}
