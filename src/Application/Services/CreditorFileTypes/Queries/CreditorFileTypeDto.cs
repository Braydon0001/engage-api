namespace Engage.Application.Services.CreditorFileTypes.Queries;

public class CreditorFileTypeDto : IMapFrom<CreditorFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFileType, CreditorFileTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorFileTypeId));
    }
}
