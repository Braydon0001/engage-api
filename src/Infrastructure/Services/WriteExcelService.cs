using OfficeOpenXml;

namespace Engage.Infrastructure.Services
{
    public class WriteExcelService
    {
        public MemoryStream GenerateExcelFileStream<T>(List<T> data, List<string> columnNames, string worksheetName, string dataStartingCell)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(worksheetName);
                //set column headings
                //Column names are optional
                if (columnNames != null)
                {
                    if (columnNames.Count > 0)
                    {
                        int colIndex = 1;
                        foreach (var row in columnNames)
                        {
                            workSheet.Cells[1, colIndex].Value = row;
                            colIndex++;
                        }

                        //load data
                        string cell = dataStartingCell.IsNullOrEmpty() ? "A2" : dataStartingCell;
                        workSheet.Cells[cell].LoadFromCollection(data, false);
                    }
                    else
                    {
                        workSheet.Cells.LoadFromCollection(data, true);
                    }
                }
                else
                {
                    workSheet.Cells.LoadFromCollection(data, true);
                }

                //TODO:::Set sub totals
                //TODO:::Sub totals are optional
                //var rowCount = workSheet.Dimension.Rows;
                //if (rowCount > 0)
                //{
                //    rowCount = rowCount + 2;
                //    if (result.ClaimSubTotals.Count > 0)
                //    {
                //        foreach (var total in result.ClaimSubTotals)
                //        {
                //            workSheet.Cells[rowCount, 1].Value = total.Name;
                //            workSheet.Cells[rowCount, 2].Value = total.Value;
                //            rowCount = rowCount + 1;
                //        }
                //    }
                //}

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}
