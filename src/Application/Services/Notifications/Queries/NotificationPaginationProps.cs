namespace Engage.Application.Services.Notifications.Queries;

public static class NotificationPaginationProps
{
    public static Dictionary<string, PaginationProperty> Create()
    {
        return new()
        {
            {"id", new PaginationProperty("NotificationId") },
            {"title", new PaginationProperty("Title") },
            {"message", new PaginationProperty("Message") },
            {"notificationTypeName", new PaginationProperty("NotificationTypeId", sortProperty : "NotificationType.Name") },
            {"startDate", new PaginationProperty("StartDate.Date") },
            {"endDate", new PaginationProperty("EndDate.Value.Date") },
        };
    }
}
