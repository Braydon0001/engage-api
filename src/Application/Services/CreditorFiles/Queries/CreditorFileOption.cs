namespace Engage.Application.Services.CreditorFiles.Queries;

public class CreditorFileOption : IMapFrom<CreditorFile>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFile, CreditorFileOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorFileId));
    }
}