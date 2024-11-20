namespace Engage.Application.Utils;

public static class FileUtils
{
    public static void ValidateReadFileOptions(ReadFileOptions options)
    {
        options.ThrowIfNull(nameof(options));
        options.File.ThrowIfNull(nameof(options.File));
        options.Folder.ThrowIfNullOrWhiteSpace(nameof(options.Folder));
    }

    public static void ValidateReadFileCounts(int rowCount, int maxRowCount)
    {
        if (rowCount == 0)
        {
            throw new ImportFileException("This file can't be imported because it contains no rows.");
        }
        if (maxRowCount > 0 && rowCount > maxRowCount)
        {
            throw new ImportFileException($"This file can't be imported because it contains too many rows.\n\n" +
                                        $"It has {rowCount} records but it is only allowed to contain {maxRowCount} rows.");
        }
    }

    public static void ValidateImportedFileCounts(int rowCount, int errorCount, int warningCount)
    {
        if (rowCount == 0)
        {
            throw new ImportFileException("This import can't be processed because there are no rows.");
        }

        if (errorCount > 0)
        {
            if (errorCount == 1)
            {
                throw new ImportFileException($"This import can't be processed because there is one error.\n\nPlease fix the error first.");
            }

            throw new ImportFileException($"This import can't be processed because there are {errorCount} errors.\n\nPlease fix the errors first.");
        }

        if (rowCount == warningCount)
        {
            throw new ImportFileException("This import can't be processed because there are only warnings.");
        }
    }
}
