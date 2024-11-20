namespace Engage.Domain.Common.Form
{
    public class BaseFormQuestionGroupEntity : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsRequired { get; set; }
        public List<JsonRule> Rules { get; set; }
        public List<JsonFile> Files { get; set; }
        public List<JsonSetting> Metadata { get; set; }
        public List<JsonLink> Links { get; set; }
    }
}
