using Z.EntityFramework.Plus;

namespace Engage.Application.Services.AuditEntryProperties.Queries;

public class AuditEntryPropertyDto : IMapFrom<AuditEntryProperty>
{
    public int Id { get; set; }
    public int AuditEntryId { get; set; }
    public string PropertyName { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AuditEntryProperty, AuditEntryPropertyDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AuditEntryPropertyID));
    }
}
