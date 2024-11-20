using Engage.Application.Services.ClaimFloatReports.Commands;
using OfficeOpenXml;

namespace Engage.WebApi.Controllers;

public class FloatReportParams
{
    public List<int> EngageRegionIds { get; set; }
    public int ClaimPeriodId { get; set; }
    public int? ToClaimPeriodId { get; set; }
}
public class ClaimFloatReportController : BaseController
{
    [HttpPost]
    public async Task<FileStreamResult> GenerateClaimFloatReport(FloatReportParams floatReportParams)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();

        using (var package = new ExcelPackage(stream))
        {
            foreach (var regionId in floatReportParams.EngageRegionIds)
            {
                var command = new GenerateClaimFloatReportCommand
                {
                    EngageRegionId = regionId,
                    ClaimPeriodId = floatReportParams.ClaimPeriodId,
                    ToClaimPeriodId = floatReportParams.ToClaimPeriodId,
                };

                var result = await Mediator.Send(command);

                var workSheet = package.Workbook.Worksheets.Add(result.RegionName);
                workSheet.Cells.LoadFromCollection(result.Data, true);

                var rowCount = workSheet.Dimension.Rows;
                if (rowCount > 0)
                {
                    rowCount = rowCount + 2;
                    if (result.ReportSubTotals.Count > 0)
                    {
                        foreach (var total in result.ReportSubTotals)
                        {
                            workSheet.Cells[rowCount, 1].Value = total.Name;
                            workSheet.Cells[rowCount, 2].Value = total.Value;
                            rowCount = rowCount + 1;
                        }
                    }
                }
            }
            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = "Claim Float Report " + DateTime.Now.ToString() + ".xlsx";

        return File(stream, contentType, fileName);

    }

    [HttpPost("topUp")]
    public async Task<FileStreamResult> GenerateClaimFloatTopUpReport(GenerateClaimFloatTopUpHistoryReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Top Up");

            workSheet.Cells.LoadFromCollection(result.Data, true);

            var rowCount = workSheet.Dimension.Rows;
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
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);

    }

}
