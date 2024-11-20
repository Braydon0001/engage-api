namespace Engage.Application.Files;

public class FileUploadOptions
{
    public string ContainerName { get; set; }
    public List<JsonFile> EntityFiles { get; set; }
    public int MaxFiles { get; set; }
    public bool OverwriteType { get; set; } = true;
    public bool SetHeaders { get; set; } = false;
}
