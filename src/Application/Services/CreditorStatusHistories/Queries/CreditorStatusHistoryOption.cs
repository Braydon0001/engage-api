namespace Engage.Application.Services.CreditorStatusHistories.Queries;

public class CreditorStatusHistoryOption : IMapFrom<CreditorStatusHistory>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatusHistory, CreditorStatusHistoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorStatusHistoryId));
    }
}