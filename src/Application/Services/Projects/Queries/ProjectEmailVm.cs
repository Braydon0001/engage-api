namespace Engage.Application.Services.Projects.Queries;

public class ProjectEmailVm : IMapFrom<CommunicationTemplate>
{
    public string ToEmailAddress { get; set; }
    public string FromEmailAddress { get; init; }
    public List<string> CcEmails { get; set; }
    public string Subject { get; init; }
    public string Body { get; init; }
    public string CreatedBy { get; set; }
    public string ProjectName { get; set; }
    public string TaskName { get; set; }
    public string StoreName { get; set; }
    public string AssignedTo { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplate, ProjectEmailVm>()
               .ForMember(d => d.ToEmailAddress, opt => opt.Ignore())
               .ForMember(d => d.FromEmailAddress, opt => opt.MapFrom(s => s.FromEmailAddress))
               .ForMember(d => d.CcEmails, opt => opt.Ignore())
               .ForMember(d => d.Subject, opt => opt.MapFrom(s => s.Subject))
               .ForMember(d => d.Body, opt => opt.MapFrom(s => s.Body))
               .ForMember(d => d.CreatedBy, opt => opt.Ignore())
               .ForMember(d => d.StoreName, opt => opt.Ignore());
    }
}
