using Engage.Application.Interfaces;
using Engage.Application.Services.PaymentReports.Commands;
using OfficeOpenXml;

namespace Engage.WebApi.Controllers;

public class PaymentReportParams
{
    //Required
    public List<int> EngageRegionIds { get; set; }
    public int PaymentStatusId { get; set; }
    public int PaymentPeriodId { get; set; }

    //Optional
    public int? CreditorId { get; set; }
    public int? ToPaymentPeriodId { get; set; }
    public int? ExpenseTypeId { get; set; }
    public DateTime? Date { get; set; }
}
public class PaymentReportController : BaseController
{
    private readonly IExcelService excel;

    public PaymentReportController(IExcelService excel)
    {
        this.excel = excel;
    }

    [HttpPost]
    public async Task<IActionResult> GeneratePaymentReport(PaymentReportParams paymentReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var regionId in paymentReportParams.EngageRegionIds)
            {
                var command = new GeneratePaymentReportCommand
                {
                    EngageRegionId = regionId,
                    PaymentStatusId = paymentReportParams.PaymentStatusId,
                    PaymentPeriodId = paymentReportParams.PaymentPeriodId,
                    CreditorId = paymentReportParams.CreditorId,
                    ToPaymentPeriodId = paymentReportParams.ToPaymentPeriodId,
                    ExpenseTypeId = paymentReportParams.ExpenseTypeId,
                    Date = paymentReportParams.Date,
                };

                var result = await Mediator.Send(command);

                var workSheet = package.Workbook.Worksheets.Add(result.RegionName);
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
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Payment Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("evo")]
    public async Task<IActionResult> EvoReport([FromBody] PaymentReportParams paymentReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var regionId in paymentReportParams.EngageRegionIds)
            {
                var command = new GeneratePaymentReportCommand
                {
                    EngageRegionId = regionId,
                    PaymentStatusId = paymentReportParams.PaymentStatusId,
                    PaymentPeriodId = paymentReportParams.PaymentPeriodId,
                    CreditorId = paymentReportParams.CreditorId,
                    ToPaymentPeriodId = paymentReportParams.ToPaymentPeriodId,
                    ExpenseTypeId = paymentReportParams.ExpenseTypeId,
                    Date = paymentReportParams.Date,
                };

                var result = await Mediator.Send(command);

                var workSheet = package.Workbook.Worksheets.Add(result.RegionName);
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
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Payment Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("fnb")]
    public async Task<IActionResult> FnbReport([FromBody] PaymentReportParams paymentReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var regionId in paymentReportParams.EngageRegionIds)
            {
                var command = new GeneratePaymentReportCommand
                {
                    EngageRegionId = regionId,
                    PaymentStatusId = paymentReportParams.PaymentStatusId,
                    PaymentPeriodId = paymentReportParams.PaymentPeriodId,
                    CreditorId = paymentReportParams.CreditorId,
                    ToPaymentPeriodId = paymentReportParams.ToPaymentPeriodId,
                    ExpenseTypeId = paymentReportParams.ExpenseTypeId,
                    Date = paymentReportParams.Date,
                };

                var result = await Mediator.Send(command);

                var workSheet = package.Workbook.Worksheets.Add(result.RegionName);
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
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "FNB Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);
    }
}
