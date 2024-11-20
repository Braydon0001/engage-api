namespace Engage.Application.Services.WebEvents.Models;

public class WebEventVm : IMapFrom<WebEvent>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public OptionDto WebEventTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebEvent, WebEventVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebEventId))
            .ForMember(d => d.WebEventTypeId, opt => opt.MapFrom(s => new OptionDto(s.WebEventTypeId, s.WebEventType.Name)));
    }
}
