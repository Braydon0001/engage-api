namespace Engage.Application.Models.FileStorage;

public class FileDto
{
    public int Id { get; set; }
    public int FileContainerId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Metadata { get; set; }
}
