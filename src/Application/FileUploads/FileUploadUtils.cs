using Engage.Domain.Common;

namespace Engage.Application.FileUploads;

public static class FileUploadUtils
{
    public static void ValidateUpload<T>(List<T> imports) where T : BaseFileUploadEntity
    {
        if (imports.Count == 0)
        {
            throw new ImportFileException("There are no rows to import.");
        }

        if (imports.Any(e => e.RowType == RowType.Error))
        {
            throw new ImportFileException("This import can't be processed because there are errors.\n\nPlease fix the errors first.");
        }
    }

}
