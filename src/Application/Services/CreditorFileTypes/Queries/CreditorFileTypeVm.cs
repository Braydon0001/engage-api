
namespace Engage.Application.Services.CreditorFileTypes.Queries;

public class CreditorFileTypeVm : IMapFrom<CreditorFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFileType, CreditorFileTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorFileTypeId));
    }
}
