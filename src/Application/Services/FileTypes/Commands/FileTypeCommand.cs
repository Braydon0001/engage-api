namespace Engage.Application.Services.FileTypes.Commands;

public class FileTypeCommand : IMapTo<FileType>
{
    public string Name { get; set; }
    public bool CanView { get; set; }
    public bool IsUrl { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileTypeCommand, FileType>();
    }
}
