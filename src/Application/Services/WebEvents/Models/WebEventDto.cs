namespace Engage.Application.Services.WebEvents.Models;

public class WebEventDto : IMapFrom<WebEvent>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int WebEventTypeId { get; set; }
    public string WebEventTypeName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebEvent, WebEventDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebEventId));
    }
}
