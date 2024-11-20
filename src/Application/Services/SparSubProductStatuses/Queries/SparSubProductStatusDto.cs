namespace Engage.Application.Services.SparSubProductStatuses.Queries;

public class SparSubProductStatusDto : IMapFrom<SparSubProductStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSubProductStatus, SparSubProductStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSubProductStatusId));
    }
}
