
using Engage.Application.Services.ExternalUserTypes.Queries;

namespace Engage.Application.Services.ProjectExternalUsers.Queries;

public class ProjectExternalUserVm : IMapFrom<ProjectExternalUser>
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string CellNumber { get; init; }
    public bool EmailPrimary { get; init; }
    public bool PhoneNumberPrimary { get; init; }
    public ExternalUserTypeOption ExternalUserTypeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectExternalUser, ProjectExternalUserVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectExternalUserId))
               .ForMember(d => d.EmailPrimary, opt => opt.MapFrom(s => s.CommunicationTypes.Any(e => e.CommunicationTypeId == (int)CommunicationTypeId.Email)))
               .ForMember(d => d.PhoneNumberPrimary, opt => opt.MapFrom(s => s.CommunicationTypes.Any(e => e.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp)))
               .ForMember(d => d.ExternalUserTypeId, opt => opt.MapFrom(s => s.ExternalUserType));
    }
}
