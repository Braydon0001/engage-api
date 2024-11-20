namespace Engage.Application.Interfaces;

public interface IImageService
{
    public Task<OperationStatus> CreatePhoto(string baseFolder, Photo photo, CancellationToken token);
    public Task<OperationStatus> BatchCreatePhoto(string baseFolder, List<Photo> photos, CancellationToken token);
}
