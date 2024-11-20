namespace Engage.Domain.Entities
{
    public class WebEvent : BaseAuditableEntity
    {
        public int WebEventId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int WebEventTypeId { get; set; }

        // Navigation Properties
        public WebEventType WebEventType { get; set; }
    }
}
