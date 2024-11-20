namespace Engage.Application.Interfaces;

public interface IFileService
{
    Task<JsonFile> UploadAsync(FileUploadCommand command, FileUploadOptions options, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(FileDeleteCommand command, string fileContainerName, CancellationToken cancellationToken);
    Task<List<JsonFile>> UpdateAsync(FileUpdateCommand command, CancellationToken cancellationToken);
    string GetFileName(FileUploadCommand command);
    string GetSas();
}
