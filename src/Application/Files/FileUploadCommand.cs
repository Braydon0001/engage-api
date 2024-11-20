namespace Engage.Application.Files;

public class FileUploadCommand
{
    public int Id { get; set; }
    public IFormFile File { get; set; }
    public string FileType { get; set; }
}
