// auto-generated
namespace Engage.Application.Services.OrderTemplates.Queries;

public class OrderTemplateOption : IMapFrom<OrderTemplate>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplate, OrderTemplateOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateId));
    }
}