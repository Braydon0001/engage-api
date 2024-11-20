// auto-generated
namespace Engage.Application.Services.OrderTemplateStatuses.Queries;

public class OrderTemplateStatusOption : IMapFrom<OrderTemplateStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateStatus, OrderTemplateStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateStatusId));
    }
}