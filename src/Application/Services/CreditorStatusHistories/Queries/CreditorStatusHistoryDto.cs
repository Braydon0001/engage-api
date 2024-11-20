namespace Engage.Application.Services.CreditorStatusHistories.Queries;

public class CreditorStatusHistoryDto : IMapFrom<CreditorStatusHistory>
{
    public int Id { get; init; }
    public int CreditorId { get; init; }
    public int CreditorStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatusHistory, CreditorStatusHistoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorStatusHistoryId));
    }
}
