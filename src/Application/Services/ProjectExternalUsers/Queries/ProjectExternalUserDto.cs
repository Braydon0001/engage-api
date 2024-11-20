namespace Engage.Application.Services.ProjectExternalUsers.Queries;

public class ProjectExternalUserDto : IMapFrom<ProjectExternalUser>
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string CellNumber { get; init; }
    public bool CommunicationEmail { get; set; }
    public bool CommunicationCellphone { get; set; }
    public string ExternalUserTypeName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectExternalUser, ProjectExternalUserDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectExternalUserId))
               .ForMember(d => d.CommunicationEmail, opt => opt.MapFrom(s => s.CommunicationTypes.Any(e => e.CommunicationTypeId == (int)CommunicationTypeId.Email)))
               .ForMember(d => d.CommunicationCellphone, opt => opt.MapFrom(s => s.CommunicationTypes.Any(e => e.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp)));
    }
}
