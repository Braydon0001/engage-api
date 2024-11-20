namespace Engage.Application.Services.ExternalUserTypes.Queries;

public class ExternalUserTypeDto : IMapFrom<ExternalUserType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExternalUserType, ExternalUserTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ExternalUserTypeId));
    }
}
