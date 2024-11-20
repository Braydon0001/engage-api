namespace Engage.Application.Services.CreditorFileTypes.Queries;

public class CreditorFileTypeOption : IMapFrom<CreditorFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFileType, CreditorFileTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorFileTypeId));
    }
}