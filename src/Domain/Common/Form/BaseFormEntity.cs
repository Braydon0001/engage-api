namespace Engage.Domain.Common.Form
{
    public class BaseFormEntity : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsRequired { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsDisabled { get; set; }
        public List<JsonFile> Files { get; set; }
        public List<JsonRule> Rules { get; set; }
        public List<JsonLink> Links { get; set; }
    }
}
