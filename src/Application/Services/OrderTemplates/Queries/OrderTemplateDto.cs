// auto-generated
namespace Engage.Application.Services.OrderTemplates.Queries;

public class OrderTemplateDto : IMapFrom<OrderTemplate>
{
    public int Id { get; set; }
    public int OrderTemplateStatusId { get; set; }
    public string OrderTemplateStatusName { get; set; }
    public int DistributionCenterId { get; set; }
    public string DistributionCenterName { get; set; }
    public string Name { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplate, OrderTemplateDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateId));
    }
}
