using ClosedXML.Excel;
using Engage.Application.Utils;
using Engage.Domain.Entities;
using OfficeOpenXml;

namespace Engage.Infrastructure.Services;

public class ExcelService : IExcelService
{
    private readonly IAppDbContext _context;

    public ExcelService(IAppDbContext context)
    {
        _context = context;
    }

    public MemoryStream CreateStream<T>(string worksheetName, IEnumerable<T> data)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add(worksheetName);
            workSheet.Cells.LoadFromCollection(data, true);
            package.Save();
        };
        stream.Position = 0;

        return stream;
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
            List<T> records = new();
            Type typeOfObject = typeof(T);

            using (IXLWorkbook workbook = new XLWorkbook(fileNameWithPath))
            {
                var worksheet = workbook.Worksheets.First();
                var properties = typeOfObject.GetProperties();

                //header column texts
                var columns = worksheet.FirstRow().Cells().Select((v, i) => new { v.Value, Index = i + 1 });//indexing in closedxml starts with 1 not from 0

                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))//Skip first row which is used for column header texts
                {
                    T obj = (T)Activator.CreateInstance(typeOfObject);
                    foreach (var prop in properties)
                    {
                        int colIndex = columns.SingleOrDefault(c => c.Value.ToString() == prop.Name.ToString()).Index;
                        var val = row.Cell(colIndex).Value;
                        var type = prop.PropertyType;
                        prop.SetValue(obj, Convert.ChangeType(val, type));
                    }
                    records.Add(obj);
                }
            }

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
        catch (Exception ex)
        {
            throw new ImportFileException("Error reading file, atleast one column name does not match the required template", ex);
        }
    }

    private static string GetFileName(ReadFileOptions options)
    {
        var fileName = options.File.FileName[..options.File.FileName.LastIndexOf(".")];
        return $"{fileName}{DateTime.Now.Ticks}.xlsx";
    }

    private static string GetFileNameWithPath(ReadFileOptions options)
    {
        var fileName = GetFileName(options);
        return Path.Combine(options.Folder, fileName);
    }
}
