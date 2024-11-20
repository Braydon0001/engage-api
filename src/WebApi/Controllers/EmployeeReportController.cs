using Engage.Application.Services.EmployeeReports.Commands;

namespace Engage.WebApi.Controllers;

using OfficeOpenXml;

public class EmployeeReportController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> GetAll(GenerateEmployeeDetailsReportCommand command)
    {
        var employeeResult = await Mediator.Send(command);

        var addressesCommand = new GenerateEmployeeAddressesReportCommand
        {
            EngageRegionIds = command.EngageRegionIds,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
        };
        var addressResult = await Mediator.Send(addressesCommand);

        var bankCommand = new GenerateEmployeeBankDetailsReportCommand
        {
            EngageRegionIds = command.EngageRegionIds,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
        };
        var bankResult = await Mediator.Send(bankCommand);

        var employmentStatusCommand = new GenerateEmployeeEmploymentStatusReportCommand
        {
            EngageRegionIds = command.EngageRegionIds,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
        };
        var employmentStatusResult = await Mediator.Send(employmentStatusCommand);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            //Employee
            var employee = package.Workbook.Worksheets.Add("Employee");
            //Load hearders
            int colIndex = 1;
            foreach (var col in employeeResult.ColumnNames)
            {
                employee.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }
            //Load data
            employee.Cells["A2"].LoadFromCollection(employeeResult.Data, false);

            //Address
            colIndex = 1;
            var address = package.Workbook.Worksheets.Add("Address");
            foreach (var col in addressResult.ColumnNames)
            {
                address.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            address.Cells["A2"].LoadFromCollection(addressResult.Data, false);

            //Bank
            colIndex = 1;
            var bank = package.Workbook.Worksheets.Add("EmployeeBankDetail");
            foreach (var col in bankResult.ColumnNames)
            {
                bank.Cells[1, colIndex].Value = col.ToString();
                colIndex = colIndex + 1;
            }

            //Load data
            bank.Cells["A2"].LoadFromCollection(bankResult.Data, false);


            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = employeeResult.ReportName + ".xlsx";

        return File(stream, contentType, fileName);
    }

    [HttpPost("terminations")]
    public async Task<FileStreamResult> GenerateTerminationsReport([FromBody] GenerateTerminationsReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Terminations");

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
