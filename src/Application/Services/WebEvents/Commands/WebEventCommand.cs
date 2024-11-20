namespace Engage.Application.Services.WebEvents.Commands;

public class WebEventCommand : IMapTo<WebEvent>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int WebEventTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebEventCommand, WebEvent>();
    }
}

