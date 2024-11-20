namespace Engage.Application.Files;

public class FileUpdateCommand : FileUploadOptions
{
    public IFormFile[] Files { get; set; }
    public int Id { get; set; }
    public string FileType { get; set; }
}