
using Engage.Application.Services.Creditors.Queries;
using Engage.Application.Services.CreditorStatuses.Queries;

namespace Engage.Application.Services.CreditorStatusHistories.Queries;

public class CreditorStatusHistoryVm : IMapFrom<CreditorStatusHistory>
{
    public int Id { get; init; }
    public CreditorOption CreditorId { get; init; }
    public CreditorStatusOption CreditorStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatusHistory, CreditorStatusHistoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorStatusHistoryId))
               .ForMember(d => d.CreditorId, opt => opt.MapFrom(s => s.Creditor))
               .ForMember(d => d.CreditorStatusId, opt => opt.MapFrom(s => s.CreditorStatus));
    }
}
