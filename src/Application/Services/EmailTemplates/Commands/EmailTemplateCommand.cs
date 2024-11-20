namespace Engage.Application.Services.EmailTemplates.Commands;

public class EmailTemplateCommand : IMapTo<EmailTemplate>
{
    public string Name { get; set; }
    public string ExternalTemplateId { get; set; }
    public string FromEmailName { get; set; }
    public string FromEmailAddress { get; set; }
    public int EmailTemplateTypeId { get; set; }
    public int EmailTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmailTemplateCommand, EmailTemplate>();
    }
}
