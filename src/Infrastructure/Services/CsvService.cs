using CsvHelper;
using CsvHelper.Configuration;
using Engage.Application.Utils;
using Engage.Domain.Entities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Engage.Infrastructure.Services;

public class CsvService : ICsvService
{
    private readonly IAppDbContext _context;

    public CsvService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ReadFileResult<T>> ReadFile<T>(ReadFileOptions options, CancellationToken cancellationToken)
    {
        try
        {
            FileUtils.ValidateReadFileOptions(options);

            if (!Directory.Exists(options.Folder))
            {
                Directory.CreateDirectory(options.Folder);
            }

            var fileName = GetFileName(options);
            var fileNameWithPath = GetFileNameWithPath(options);

            // Create a local copy of the file
            using var stream = File.Create(fileNameWithPath);
            await options.File.CopyToAsync(stream, cancellationToken);
            stream.Close();

            // Read the file into a collection
            using var reader = File.OpenText(fileNameWithPath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = (args) => args.Header.ToLower(),
            };
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<T>().ToList();
            reader.Close();

            FileUtils.ValidateReadFileCounts(records.Count, options.MaxRows);

            var fileUpload = new FileUpload { FileName = fileName };
            if (options.DoFileUpload)
            {

                _context.FileUploads.Add(fileUpload);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new ReadFileResult<T>
            {
                FileName = fileName,
                Data = records,
                FileUploadId = fileUpload.FileUploadId,
            };
        }
        catch (HeaderValidationException ex)
        {
            var regex = new Regex("Header with name(.+) was not found");
            if (regex.IsMatch(ex.Message) && ex.InvalidHeaders.Length == 1)
            {
                throw new ImportFileException($"The header with name '{ex.InvalidHeaders[0]}' was not found.\n\nPlease add the header.", ex);
            }

            throw new ImportFileException("Error reading csv file", ex);
        }
        catch (Exception ex) when (ex is not ImportFileException)
        {
            throw new ImportFileException("Error reading csv file", ex);
        }
    }

    private string GetFileName(ReadFileOptions options)
    {
        var fileName = options.File.FileName.Substring(0, options.File.FileName.LastIndexOf("."));
        return $"{fileName}{DateTime.Now.Ticks}.csv";
    }

    private string GetFileNameWithPath(ReadFileOptions options)
    {
        var fileName = GetFileName(options);
        return Path.Combine(options.Folder, fileName);
    }
}
