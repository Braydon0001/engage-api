namespace Engage.Application.Services.OrderTemplateGroups.Queries;

public class OrderTemplateGroupDto : IMapFrom<OrderTemplateGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateGroup, OrderTemplateGroupDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateGroupId));
    }
}
