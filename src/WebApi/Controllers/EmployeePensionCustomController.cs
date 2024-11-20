using Engage.Application.Interfaces;
using Engage.Application.Services.EmployeePensions.Commands;
using Engage.Application.Services.EmployeePensions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeePensionController : BaseController
{
    private readonly IExcelService excel;

    public EmployeePensionController(IExcelService excel)
    {
        this.excel = excel;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeePensionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeePensionVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("files")]
    public async Task<ActionResult<ListResult<EmployeePensionFileDto>>> DtoList([FromQuery] EmployeePensionFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeePensionFileDto>(entities));
    }

    [HttpPost("report")]
    public async Task<FileStreamResult> EmployeePensionReport([FromBody] EmployeePensionReportCommand command)
    {
        var result = await Mediator.Send(command);

        var stream = excel.CreateStream("EMPLOYEE PENSION", result.Data);
        var fileName = result.ReportName + ".xlsx";

        return File(stream, "application/octet-stream", fileName);

    }

    [HttpPost("nopensionreport")]
    public async Task<FileStreamResult> EmployeesWithoutPensionReport([FromBody] EmployeesWithoutPensionReportCommand command)
    {
        var result = await Mediator.Send(command);

        var stream = excel.CreateStream("NO PENSION", result.Data);
        var fileName = result.ReportName + ".xlsx";

        return File(stream, "application/octet-stream", fileName);

    }
}