﻿using Engage.Application.Services.EmployeeSuspensions.Commands;
using Engage.Application.Services.EmployeeSuspensions.Models;
using Engage.Application.Services.EmployeeSuspensions.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeSuspensionController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeSuspensionDto>>> GetAllByEmployee([FromRoute] EmployeeSuspensionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeSuspensionVm>> GetVm([FromRoute] EmployeeSuspensionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeSuspensionCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeSuspensionUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EmployeeSuspensionUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeSuspensionDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
