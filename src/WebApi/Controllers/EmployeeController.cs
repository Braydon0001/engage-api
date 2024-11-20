using Engage.Application.Services.Employees.Commands;
using Engage.Application.Services.Employees.Models;
using Engage.Application.Services.Employees.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeListDto>>> GetAll([FromRoute] EmployeesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<EmployeeListDto>>> PaginatedQuery(EmployeePaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<IEnumerable<EmployeeOption>>> PaginatedOptionQuery([FromQuery] EmployeePaginatedOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("disablelist")]
    public async Task<ActionResult<ListResult<EmployeeListDto>>> GetAllPastEndDate([FromRoute] EmployeesToDisableQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<ListResult<EmployeeListDto>>> GetManagers([FromRoute] EmployeeManagersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetById([FromRoute] EmployeeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("vm/{id}")]
    public async Task<ActionResult<EmployeeVm>> GetVm([FromRoute] EmployeeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("contract/vm/{id}")]
    public async Task<ActionResult<EmployeeVm>> GetContractVm([FromRoute] EmployeeVmQuery query)
    {
        query.IsContract = true;
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("viewmodel/{id}")]
    public async Task<ActionResult<EmployeeVm>> GetViewModel([FromRoute] EmployeeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employeeassets/{id}")]
    public async Task<ActionResult<EmployeeAssetsVM>> GetAssets([FromRoute] EmployeeAssetsVMQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employeeregions")]
    public async Task<IActionResult> GetEmployeeEngageRegions([FromRoute] EmployeeEngageRegionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }



    [HttpGet("options/with-disabled")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsWithDisabled([FromRoute] EmployeeOptionsWithDisabledQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromRoute] EmployeeOptionsQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/manager")]
    public async Task<ActionResult<List<OptionDto>>> GetManagerOptions([FromRoute] EmployeeOptionSubordinatesByManagerQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/regional")]
    public async Task<ActionResult<List<OptionDto>>> EmployeesByRegion([FromRoute] EmployeeOptionEmployeeRegionQuery query, [FromQuery] string search, CancellationToken cancellationToken)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("option/productwarehouse/{productwarehouseId}")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByProductWarehouse([FromRoute] EmployeeOptionWarehouseRegionQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/engageregionid/{engageRegionId}")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByRegion([FromRoute] RegionEmployeeOptionsQuery query, [FromQuery] string search, [FromRoute] int engageRegionId)
    {
        query.Search = search;
        query.EngageRegionId = engageRegionId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/voucherId/{voucherid}/")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByEmployee([FromQuery] VoucherEmployeeOptionsQuery query, [FromRoute] int voucherId)
    {
        query.VoucherId = voucherId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("history/employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeHistoryDto>>> GetAllByEmployee([FromRoute] EmployeeHistoryListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
    {
        var result = Ok(await Mediator.Send(command));

        return result;
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("contacts")]
    public async Task<IActionResult> UpdateContacts(UpdateEmployeeContactsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("details")]
    public async Task<IActionResult> UpdateDetails(UpdateEmployeeDetailsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("exemptions")]
    public async Task<IActionResult> UpdateExemptions(UpdateEmployeeExemptionsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("personal")]
    public async Task<IActionResult> UpdatePersonal(UpdateEmployeePersonalCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reassignassets")]
    public async Task<IActionResult> UpdateAssets(UpdateEmployeeAssignedAssetsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("tax")]
    public async Task<IActionResult> UpdateTax(UpdateEmployeeTaxCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("terminate")]
    public async Task<IActionResult> UpdateTerminate(UpdateEmployeeTerminateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reinstate")]
    public async Task<IActionResult> UpdateReinstate(UpdateEmployeeReinstateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("batch/disable")]
    public async Task<IActionResult> BatchDisableEmployees(DisableEmployeesCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("toggledisabled")]
    public override async Task<IActionResult> ToggleDisabled(DisableCommand disableCommand)
    {
        return Ok(await Mediator.Send(new ToggleDisabledEmployeeCommand
        {
            Id = disableCommand.Id
        }));
    }

    [HttpPut("grouprisk")]
    public async Task<IActionResult> UpdateGroupRisk(UpdateEmployeeGroupRiskCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("contract")]
    public async Task<IActionResult> UpdateContract(UpdateEmployeeContractCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EmployeeUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
