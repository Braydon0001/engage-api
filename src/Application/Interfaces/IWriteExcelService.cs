namespace Engage.Application.Interfaces;

public interface IWriteExcelService
{
    MemoryStream GenerateExcelFileStream<T>(List<T> data, List<string> columnNames, string worksheetName, string dataStartingCell) where T : class;
}
