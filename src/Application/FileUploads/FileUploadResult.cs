using Engage.Domain.Common;

namespace Engage.Application.FileUploads;

public class FileUploadResult<T> where T : BaseFileUploadEntity
{
    public IEnumerable<T> Data { get; set; }
    public string FileName { get; set; }
    public int SuccessCount { get; set; }
    public int ErrorCount { get; set; }
    public int WarningCount { get; set; }

    public FileUploadResult(IEnumerable<T> data, string fileName)
    {
        Data = data;
        FileName = fileName;
        SuccessCount = data.Where(e => e.RowType == RowType.Success).Count();
        ErrorCount = data.Where(e => e.RowType == RowType.Error).Count();
        WarningCount = data.Where(e => e.RowType == RowType.Warning).Count();
    }

}
