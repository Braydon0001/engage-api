using Engage.Application.Services.VoucherDetails.Commands;
using Engage.Application.Services.VoucherDetails.Models;
using Engage.Application.Services.VoucherDetails.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("claim")]
public class VoucherDetailController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<VoucherDetailDto>>> GetAllAvailable([FromRoute] GetAllAvailableVouchersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("assigned")]
    public async Task<ActionResult<ListResult<VoucherDetailDto>>> GetAllAssigned([FromRoute] GetAllAssignedVouchersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("voucher/{voucherid}")]
    public async Task<ActionResult<ListResult<VoucherDetailDto>>> GetAllByVoucher([FromRoute] GetVoucherDetailsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("employee/{employeeid}/{voucherstatusid}")]
    public async Task<ActionResult<ListResult<VoucherDetailDto>>> GetAllByEmployee([FromRoute] GetEmployeeVoucherDetailsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("available/engageregionid/{engageregionid}/")]
    public async Task<ActionResult<ListResult<VoucherDetailDto>>> GetAllByRegionAvailable([FromRoute] int? engageRegionId)
    {
        return Ok(await Mediator.Send(new GetAllAvailableVouchersQuery
        {
            EngageRegionId = engageRegionId,
        }));
    }

    [HttpGet("assigned/engageregionid/{engageregionid}/")]
    public async Task<ActionResult<VoucherDetailDto>> GetAllByFilters([FromRoute] int? engageRegionId)
    {
        return Ok(await Mediator.Send(new AssignedVouchersQuery
        {
            EngageRegionId = engageRegionId,
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VoucherDetailVm>> GetVm([FromRoute] GetVoucherDetailVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVoucherDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("approve")]
    public async Task<IActionResult> BatchProcessVoucherDetail([FromBody] BatchProcessVoucherDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(DeleteVoucherDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateVoucherDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] VoucherDetailUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new VoucherDetailDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpPut("amount")]
    public async Task<IActionResult> UpdateVoucherDetailAmount(UpdateVoucherDetailAmountCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("note")]
    public async Task<IActionResult> UpdateVoucherDetailNote(UpdateVoucherDetailNoteCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("close")]
    public async Task<IActionResult> CloseVoucherDetail(CloseVoucherDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("employee")]
    public async Task<IActionResult> UpdateVoucherDetailEmployee(UpdateVoucherDetailEmployeeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
