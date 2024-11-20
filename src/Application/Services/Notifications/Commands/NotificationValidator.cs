namespace Engage.Application.Services.Notifications.Commands;

public class NotificationValidator<T> : AbstractValidator<T> where T : NotificationCommand
{
    public NotificationValidator()
    {
        RuleFor(x => x.NotificationTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Title).MaximumLength(220).NotEmpty();
        RuleFor(x => x.Message).MaximumLength(1500).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
    }
}

public class CreateNotificationValidator : NotificationValidator<NotificationInsertCommand>
{
}

public class UpdateNotificationValidator : NotificationValidator<NotificationUpdateCommand>
{
    public UpdateNotificationValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
