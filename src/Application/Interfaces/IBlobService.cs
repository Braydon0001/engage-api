namespace Engage.Application.Interfaces;

public interface IBlobService
{
    string CreateFileName(int id, string fileName);

    Task<Uri> UploadAsync(Stream fileStream, string folderName, string fileName, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(string folderName, string fileName, CancellationToken cancellationToken);
}
