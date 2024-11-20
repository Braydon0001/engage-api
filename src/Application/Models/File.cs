using Microsoft.AspNetCore.Http;

namespace Engage.Application.Models
{
    public class ReadFileOptions
    {
        public IFormFile File { get; set; }
        public string Folder { get; set; }
        public int MaxRows { get; set; }
        public bool DoFileUpload { get; set; }
    }

    public class ReadFileResult<T>
    {
        public List<T> Data { get; set; }
        public string FileName { get; set; }
        public int FileUploadId { get; set; }
    }

    public class ImportFileResult<T>
    {
        public List<T> Data { get; set; }
        public string FileName { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }
    }
}
