using Engage.Application.Services.DistributionCenters.Queries;
using Engage.Application.Services.OrderTemplateGroups.Queries;
using Engage.Application.Services.OrderTemplateStatuses.Queries;

namespace Engage.Application.Services.OrderTemplates.Queries;

public class OrderTemplateVm : IMapFrom<OrderTemplate>
{
    public int Id { get; set; }
    public OrderTemplateStatusOption OrderTemplateStatusId { get; set; }
    public DistributionCenterOption DistributionCenterId { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<OrderTemplateGroupVm> OrderTemplateGroups { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplate, OrderTemplateVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateId))
               .ForMember(d => d.OrderTemplateStatusId, opt => opt.MapFrom(s => s.OrderTemplateStatus))
               .ForMember(d => d.DistributionCenterId, opt => opt.MapFrom(s => s.DistributionCenter))
               .ForMember(d => d.OrderTemplateGroups, opt => opt.MapFrom(s => s.OrderTemplateGroups));
    }
}
