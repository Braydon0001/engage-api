using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Notifications.Commands;

public static class NotificationAssigns
{
    public static async Task BatchAssign(IMediator mediator, NotificationCommand command, Notification entity)
    {
        if (command.NotificationChannelIds != null)
        {
            await mediator.Send(new BatchAssignCommand(
            AssignDesc.NOTIFICATION_CHANNEL_NOTIFICATION, entity.NotificationId, command.NotificationChannelIds));
        }
    }
}
