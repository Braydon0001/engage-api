namespace Engage.Application.Services.CreditorFiles.Queries;

public class CreditorFileDto : IMapFrom<CreditorFile>
{
    public int Id { get; init; }
    public int CreditorId { get; init; }
    public int CreditorFileTypeId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorFile, CreditorFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorFileId));
    }
}
