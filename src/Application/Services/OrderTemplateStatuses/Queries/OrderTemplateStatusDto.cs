// auto-generated
namespace Engage.Application.Services.OrderTemplateStatuses.Queries;

public class OrderTemplateStatusDto : IMapFrom<OrderTemplateStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateStatus, OrderTemplateStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateStatusId));
    }
}
