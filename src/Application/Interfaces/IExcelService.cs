namespace Engage.Application.Interfaces;

public interface IExcelService
{
    public MemoryStream CreateStream<T>(string worksheetName, IEnumerable<T> data);
    public Task<ReadFileResult<T>> ReadFile<T>(ReadFileOptions readFileOptions, CancellationToken cancellationToken);
}
