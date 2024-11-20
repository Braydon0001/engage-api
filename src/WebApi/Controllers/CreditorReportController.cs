using Engage.Application.Services.CreditorReports.Commands;
using OfficeOpenXml;

namespace Engage.WebApi.Controllers;

public class CreditorReportParams
{
    //Required
    public int CreditorStatusId { get; set; }
    public int PaymentPeriodId { get; set; }

    //Optional
    public int? ToPaymentPeriodId { get; set; }
    public DateTime? Date { get; set; }
}
public class CreditorReportController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> GenerateCreditorReport(CreditorReportParams creditorReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            var command = new GenerateCreditorReportCommand
            {
                CreditorStatusId = creditorReportParams.CreditorStatusId,
                PaymentPeriodId = creditorReportParams.PaymentPeriodId,
                ToPaymentPeriodId = creditorReportParams.ToPaymentPeriodId,
                Date = creditorReportParams.Date,
            };

            var result = await Mediator.Send(command);

            var workSheet = package.Workbook.Worksheets.Add("Creditors");
            workSheet.Cells.LoadFromCollection(result.Data, true);

            var rowCount = workSheet.Dimension.Rows;
            if (rowCount > 0)
            {
                rowCount += 2;
                if (result.ReportSubTotals.Count > 0)
                {
                    foreach (var total in result.ReportSubTotals)
                    {
                        workSheet.Cells[rowCount, 1].Value = total.Name;
                        workSheet.Cells[rowCount, 2].Value = total.Value;
                        rowCount++;
                    }
                }
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Creditor Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }
}
