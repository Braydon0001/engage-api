namespace Engage.Domain.Entities;

public class EmailTemplate : BaseAuditableEntity
{
    public int EmailTemplateId { get; set; }
    public string Name { get; set; }
    public string ExternalTemplateId { get; set; }
    public string FromEmailName { get; set; }
    public string FromEmailAddress { get; set; }
    public int EmailTemplateTypeId { get; set; }
    public int EmailTypeId { get; set; }

    // Navifgation Properties 
    public EmailTemplateType EmailTemplateType { get; set; }
    public EmailType EmailType { get; set; }
}
