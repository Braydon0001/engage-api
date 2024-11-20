namespace Engage.Domain.Common.Form
{
    public class BaseFormAnswerEntity : BaseAuditableEntity
    {
        public string AnswerText { get; set; }
        public List<JsonFile> Files { get; set; }
        public List<JsonSetting> Metadata { get; set; }
        public DateTime? AnswerDate { get; set; }
    }
}
