namespace Engage.Application.Services.SparProductStatuses.Queries;

public class SparProductStatusDto : IMapFrom<SparProductStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProductStatus, SparProductStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparProductStatusId));
    }
}
