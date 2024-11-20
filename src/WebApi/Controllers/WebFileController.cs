using Engage.Application.Services.WebFiles.Commands;
using Engage.Application.Services.WebFiles.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebFileController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<WebFileDto>>> PaginatedQuery(WebFilePaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<List<WebFileOption>>> PaginatedOptionQuery(WebFilePaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpPost("options")]
    public async Task<ActionResult<IEnumerable<WebFileOption>>> OptionList([FromQuery] int? webFileCategoryId, [FromQuery] int? fileTypeId, CancellationToken cancellationToken)
    {
        var queryable = Queryable(webFileCategoryId, fileTypeId);

        var entities = await queryable.OrderBy(e => e.DisplayName)
                                      .ProjectTo<WebFileOption>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return Ok(entities);
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] WebFileFileUploadCommand command)
    {
        var file = await Mediator.Send(command);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new WebFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    private IQueryable<WebFile> Queryable(int? webFileCategoryId = default, int? fileTypeId = default)
    {
        var queryable = Context.WebFiles.AsQueryable().AsNoTracking();

        if (webFileCategoryId.HasValue)
        {
            queryable = queryable.Where(e => e.WebFileCategoryId == webFileCategoryId);
        }

        if (fileTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.FileTypeId == fileTypeId);
        }

        return queryable;
    }

}