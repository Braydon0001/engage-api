using Engage.Application.Services.Vouchers.Commands;
using Engage.Application.Services.Vouchers.Models;
using Engage.Application.Services.Vouchers.Queries;
using OfficeOpenXml;

namespace Engage.WebApi.Controllers;

[Authorize("claim")]
public class VoucherController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<VoucherSubTotalDto>>> PaginatedQuery(VoucherPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VoucherVm>> GetVm([FromRoute] VoucherVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("voucherreports")]
    public async Task<FileStreamResult> GenerateVoucherReport([FromBody] GenerateVoucherReportCommand command)
    {

        var recievedResult = await Mediator.Send(new GenerateVoucherReportCommand
        {
            ClaimPeriodId = command.ClaimPeriodId,
            EngageRegionIds = command.EngageRegionIds,
            StatusNumber = VoucherDetailStatusId.Received
        });

        var assignedResult = await Mediator.Send(new GenerateVoucherReportCommand
        {
            ClaimPeriodId = command.ClaimPeriodId,
            EngageRegionIds = command.EngageRegionIds,
            StatusNumber = VoucherDetailStatusId.Assigned
        });

        var issuedResult = await Mediator.Send(new GenerateVoucherReportCommand
        {
            ClaimPeriodId = command.ClaimPeriodId,
            EngageRegionIds = command.EngageRegionIds,
            StatusNumber = VoucherDetailStatusId.Issued
        });

        var allClaimVouchersResult = await Mediator.Send(new GenerateClaimVoucherReportCommand
        {
            ClaimPeriodId = command.ClaimPeriodId,
            EngageRegionIds = command.EngageRegionIds,
        });

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            //Received vouchers page
            var workSheetRecieved = package.Workbook.Worksheets.Add("Received");
            var workSheetAssigned = package.Workbook.Worksheets.Add("Assigned");
            var workSheetIssued = package.Workbook.Worksheets.Add("Issued");
            var workSheetAllClaimVouchers = package.Workbook.Worksheets.Add("All Claim Vouchers");

            int colIndex = 1;
            foreach (var col in recievedResult.ColumnNames)
            {
                workSheetRecieved.Cells[1, colIndex].Value = col.ToString();
                colIndex++;
            }
            //Assigned vouchers page
            colIndex = 1;
            foreach (var col in assignedResult.ColumnNames)
            {
                workSheetAssigned.Cells[1, colIndex].Value = col.ToString();
                colIndex++;
            }
            //Issued vouchers page
            colIndex = 1;
            foreach (var col in issuedResult.ColumnNames)
            {
                workSheetIssued.Cells[1, colIndex].Value = col.ToString();
                colIndex++;
            }

            //All Claim Vouchers page
            workSheetAllClaimVouchers.Cells.LoadFromCollection(allClaimVouchersResult.Data, true);
            var rowCount = workSheetAllClaimVouchers.Dimension.Rows;
            if (rowCount > 0)
            {
                rowCount = rowCount + 2;
                if (allClaimVouchersResult.ReportSubTotals.Count > 0)
                {
                    foreach (var total in allClaimVouchersResult.ReportSubTotals)
                    {
                        workSheetAllClaimVouchers.Cells[rowCount, 1].Value = total.Name;
                        workSheetAllClaimVouchers.Cells[rowCount, 2].Value = total.Value;
                        rowCount = rowCount + 1;
                    }
                }
            }
            //load data
            workSheetRecieved.Cells["A2"].LoadFromCollection(recievedResult.Data, false);

            workSheetAssigned.Cells["A2"].LoadFromCollection(assignedResult.Data, false);

            workSheetIssued.Cells["A2"].LoadFromCollection(issuedResult.Data, false);

            package.Save();
        }

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = issuedResult.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVoucherCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateVoucherCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
