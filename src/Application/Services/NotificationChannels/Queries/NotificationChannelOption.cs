namespace Engage.Application.Services.NotificationChannels.Queries;

public class NotificationChannelOption : IMapFrom<NotificationChannel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<NotificationChannel, NotificationChannelOption>();
    }
}