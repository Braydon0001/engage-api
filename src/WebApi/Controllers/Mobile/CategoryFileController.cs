using Engage.Application.Services.CategoryFileEmployees.Queries;
using Engage.Application.Services.CategoryFiles.Commands;
using Engage.Application.Services.CategoryFiles.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class CategoryFileController : BaseMobileController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryFileVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CategoryFileVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("GetByStoreAndEmployee")]
    public async Task<ActionResult<ListResult<CategoryFileDto>>> GetByStoreAndEmployee([FromRoute] CategoryFileStoreQuery query, [FromQuery] string search, [FromQuery] List<int> fileTypeId, [FromQuery] int employeeId, [FromQuery] int storeId, CancellationToken cancellationToken)
    {
        query.Search = search;
        query.EmployeeId = employeeId;
        query.StoreId = storeId;
        query.FileTypeId = fileTypeId;

        return Ok(await Mediator.Send(query));
    }

    [HttpGet("GetTargeted")]
    public async Task<ActionResult<CategoryFileAdvancedTargetingVm>> GetTargetedByParams([FromQuery] string search, [FromQuery] List<int> fileTypeId, [FromQuery] int employeeId, [FromQuery] int storeId, [FromQuery] DateTime date)
    {
        if (employeeId <= 0 || storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var result = await Mediator.Send(new CategoryFileTargetedParamsQuery(employeeId, storeId, date, fileTypeId, search));

        return result == null ? NotFound() : Ok(result);
    }


    [HttpPost("page")]
    public async Task<ActionResult<ListResult<CategoryFileDto>>> Paginated(CategoryFilePaginatedQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<IEnumerable<CategoryFileOption>>> PaginatedOption(CategoryFilePaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CategoryFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CategoryFileId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CategoryFileId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] CategoryFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CategoryFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
