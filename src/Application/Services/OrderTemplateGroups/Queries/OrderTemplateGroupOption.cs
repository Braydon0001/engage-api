namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupOption : IMapFrom<OrderTemplateGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateGroup, OrderTemplateGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateGroupId));
    }
}