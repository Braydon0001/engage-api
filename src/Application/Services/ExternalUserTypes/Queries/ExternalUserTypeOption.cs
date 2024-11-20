namespace Engage.Application.Services.ExternalUserTypes.Queries;

public class ExternalUserTypeOption : IMapFrom<ExternalUserType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExternalUserType, ExternalUserTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ExternalUserTypeId));
    }
}