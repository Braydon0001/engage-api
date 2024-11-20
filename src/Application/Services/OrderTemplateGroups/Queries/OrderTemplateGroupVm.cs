using Engage.Application.Services.OrderTemplateProducts.Queries;

namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupVm : IMapFrom<OrderTemplateGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public List<OrderTemplateProductVm> OrderTemplateProducts { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateGroup, OrderTemplateGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateGroupId))
               .ForMember(d => d.OrderTemplateProducts, opt => opt.MapFrom(s => s.OrderTemplateProducts.Where(o => o.Disabled == false)));
    }
}
