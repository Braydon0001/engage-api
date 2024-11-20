namespace Engage.WebApi.Controllers;

using Engage.Application.Services.EmployeeTransactions.Commands;
using OfficeOpenXml;

public partial class EmployeeTransactionReportController : BaseController
{
    [HttpPost("overtime")]
    public async Task<FileStreamResult> GenerateOvertimeReport([FromBody] EmployeeTransactionOvertimeReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Overtime");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex++;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("backpay")]
    public async Task<FileStreamResult> GenerateBackpayReport([FromBody] EmployeeTransactionBackpayReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Back Pay");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("unpaid")]
    public async Task<FileStreamResult> GenerateUnpaidReport([FromBody] EmployeeTransactionUnpaidReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Unpaid Time");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("reimbursement")]
    public async Task<FileStreamResult> GenerateReimbursementReport([FromBody] EmployeeTransactionReimbursementReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Reimbursement");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("amount")]
    public async Task<FileStreamResult> GenerateAmountReport([FromBody] EmployeeTransactionAmountReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Amount Allowance");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("travelallowancerecurring")]
    public async Task<FileStreamResult> GenerateTravelAllowanceRecurringReport([FromBody] EmployeeTransactionTravelAllowanceRecurringReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Travel Allowance Recurring");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("cellphoneallowancerecurring")]
    public async Task<FileStreamResult> GenerateCellPhoneAllowanceRecurringReport([FromBody] EmployeeTransactionCellPhoneAllowanceRecurringReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Travel Allowance Recurring");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("amountdeduction")]
    public async Task<FileStreamResult> GenerateAmountDeductionReport([FromBody] EmployeeTransactionAmountDeductionReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Amount Deduction");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("amountdeductionrecurring")]
    public async Task<FileStreamResult> GenerateAmountDeductionRecurringReport([FromBody] EmployeeTransactionAmountDeductionRecurringReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Amount Deduction Recurring");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("loandeductionrecurring")]
    public async Task<FileStreamResult> GenerateLoanDeductionRecurringReport([FromBody] EmployeeTransactionLoanDeductionRecurringReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Loan Deduction Recurring");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("creditdeductionrecurring")]
    public async Task<FileStreamResult> GenerateCreditDeductionRecurringReport([FromBody] EmployeeTransactionCreditDeductionRecurringReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Credit Deduction Recurring");

            int colIndex = 1;
            foreach (var col in result.ColumnNames)
            {
                workSheet.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            workSheet.Cells["A2"].LoadFromCollection(result.Data, false);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }
}
