namespace Engage.Application.Services.FileContainers.Commands;

public class FileContainerCommand : IMapTo<FileContainer>
{
    public string Name { get; set; }
    public string ContainerName { get; set; }
    public bool PublicAccess { get; set; }
    public string FileNameStrategy { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileContainerCommand, FileContainer>();
    }
}
