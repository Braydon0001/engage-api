
namespace Engage.Application.Services.ExternalUserTypes.Queries;

public class ExternalUserTypeVm : IMapFrom<ExternalUserType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExternalUserType, ExternalUserTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ExternalUserTypeId));
    }
}
