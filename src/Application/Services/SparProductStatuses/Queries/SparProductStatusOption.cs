namespace Engage.Application.Services.SparProductStatuses.Queries;

public class SparProductStatusOption : IMapFrom<SparProductStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProductStatus, SparProductStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparProductStatusId));
    }
}