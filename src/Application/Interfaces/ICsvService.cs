namespace Engage.Application.Interfaces;

public interface ICsvService
{
    public Task<ReadFileResult<T>> ReadFile<T>(ReadFileOptions readFileOptions, CancellationToken cancellationToken);
}
