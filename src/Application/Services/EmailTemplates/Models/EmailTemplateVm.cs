namespace Engage.Application.Services.EmailTemplates.Models;

public class EmailTemplateVm : IMapFrom<EmailTemplate>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ExternalTemplateId { get; set; }
    public string FromEmailName { get; set; }
    public string FromEmailAddress { get; set; }
    public OptionDto EmailTemplateTypeId { get; set; }
    public OptionDto EmailTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmailTemplate, EmailTemplateVm>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.EmailTemplateId))
            .ForMember(e => e.EmailTemplateTypeId, opt => opt.MapFrom(d => new OptionDto(d.EmailTemplateTypeId, d.EmailTemplateType.Name)))
            .ForMember(e => e.EmailTypeId, opt => opt.MapFrom(d => new OptionDto(d.EmailTypeId, d.EmailType.Name)));
    }
}
