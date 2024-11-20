using Engage.Application.Services.ClaimReports.Commands;

namespace Engage.WebApi.Controllers;
using OfficeOpenXml;

public class ClaimReportPreviewController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] ClaimPaymentPreviewListCommand command)
    {
        var result = Ok(await Mediator.Send(command));

        return result;
    }

    [HttpPost("related")]
    public async Task<IActionResult> GetRelated([FromBody] ClaimPaymentPreviewRelatedListCommand command)
    {
        var result = Ok(await Mediator.Send(command));

        return result;
    }

    [HttpPost("PreviewFNBReport")]
    public async Task<FileStreamResult> PayClaims([FromBody] PreviewClaimsCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Claims");
            workSheet.Cells[1, 1].Value = result.ReportTitle.ToString();
            workSheet.Cells[2, 1].Value = DateTime.Now.Date.ToShortDateString();
            workSheet.Cells[3, 1].Value = result.ReportAccountNumber.ToString();

            workSheet.Cells["A4"].LoadFromCollection(result.Data, true);

            var rowCount = workSheet.Dimension.Rows;

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);

    }

    [HttpPost("PayClaimsAndSendNotifications")]
    public async Task<IActionResult> PayClaimsAndSendNotifications([FromBody] PayClaimsAndSendNotificationsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
