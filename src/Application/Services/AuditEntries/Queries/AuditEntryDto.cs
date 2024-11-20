using Engage.Application.Services.AuditEntryProperties.Queries;
using Z.EntityFramework.Plus;

namespace Engage.Application.Services.AuditEntries.Queries;

public class AuditEntryDto : IMapFrom<AuditEntry>
{
    public int Id { get; set; }
    public string EntitySetName { get; set; }
    public string EntityTypeName { get; set; }
    public string StateName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<AuditEntryPropertyDto> AuditEntryProperties { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AuditEntry, AuditEntryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AuditEntryID))
               .ForMember(d => d.AuditEntryProperties,
                     opt => opt.MapFrom(s => s.Properties
                     .OrderByDescending(e => e.AuditEntryPropertyID)
                     .Select(e => new AuditEntryPropertyDto()
                     {
                         Id = e.AuditEntryPropertyID,
                         AuditEntryId = e.AuditEntryID,
                         PropertyName = e.PropertyName,
                         OldValue = e.OldValue != null ? e.OldValue.ToString() : "",
                         NewValue = e.NewValue != null ? e.NewValue.ToString() : "",
                     })
                     .ToList()
                ));
    }
}
