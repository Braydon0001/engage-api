using Engage.Application.Services.ClaimInvoices.Commands;
using OfficeOpenXml;

namespace Engage.WebApi.Controllers;

public class ClaimInvoiceController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] ClaimInvoicePreviewListCommand command)
    {
        var result = Ok(await Mediator.Send(command));

        return result;
    }

    [HttpPost("GenerateClaimInvoice")]
    public async Task<FileStreamResult> GenerateClaimInvoice([FromBody] GenerateClaimInvoiceCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            //Load hearders
            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            var rowCount = workSheet.Dimension.Rows;

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);

    }
}
