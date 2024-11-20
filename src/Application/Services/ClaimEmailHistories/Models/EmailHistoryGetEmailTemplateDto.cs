namespace Engage.Application.Services.ClaimEmailHistories.Models;

public class EmailHistoryGetEmailTemplateDto : IMapFrom<EmailHistory>
{
    public int Id { get; set; }
    public int EmailTemplateId { get; set; }
    public int EmailTypeId { get; set; }
    public EmailHistoryTemplateVariable EmailHistoryTemplateVariables { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmailHistory, EmailHistoryGetEmailTemplateDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmailHistoryId))
            .ForMember(d => d.EmailTemplateId, opt => opt.MapFrom(s => s.EmailTemplateId))
            .ForMember(d => d.EmailTypeId, opt => opt.MapFrom(s => s.EmailTemplate.EmailTypeId))
            .ForMember(d => d.EmailHistoryTemplateVariables, opt => opt.Ignore());
    }
}
