using Engage.Application.Services.EmployeeEmployeeBadges.Commands;
using Engage.Application.Services.Employees.Commands;
using Engage.Application.Services.EmployeeTrainings.Commands;
using Engage.Application.Services.EmployeeTrainings.Models;
using Engage.Application.Services.EmployeeTrainings.Queries;
using OfficeOpenXml;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeTrainingController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeTrainingDto>>> GetAll([FromRoute] EmployeeTrainingsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeTrainingCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{TrainingId}/{EmployeeId}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteEmployeeTrainingCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("report")]
    public async Task<FileStreamResult> GenerateEmployeeTrainingReport([FromBody] GenerateEmployeeTrainingReportCommand command)
    {
        var result = await Mediator.Send(command);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("TRAINING EMPLOYEE LOG");

            workSheet.Cells.LoadFromCollection(result.Data, true);

            package.Save();
        };

        stream.Position = 0;
        var contentType = "application/octet-stream";
        var fileName = result.ReportName + ".xlsx";

        return File(stream, contentType, fileName);

    }    
}
