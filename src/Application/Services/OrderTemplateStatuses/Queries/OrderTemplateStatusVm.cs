// auto-generated
namespace Engage.Application.Services.OrderTemplateStatuses.Queries;

public class OrderTemplateStatusVm : IMapFrom<OrderTemplateStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateStatus, OrderTemplateStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateStatusId));
    }
}
