namespace Engage.Application.Services.ClaimEmailHistories.Models;
public class ClaimEmailHistoryDto : IMapFrom<EmailHistory>
{
    public int Id { get; set; }
    public int EmailTemplateId { get; set; }
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string UserName { get; set; }
    public string Error { get; set; }
    public DateTime Created { get; set; }
    public string EmailTemplateName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmailHistory, ClaimEmailHistoryDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmailHistoryId))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.EmailTemplateName, opt => opt.MapFrom(s => s.EmailTemplate.Name));
    }
}
