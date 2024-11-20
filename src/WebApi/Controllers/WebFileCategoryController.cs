using Engage.Application.Services.WebFileCategories.Commands;
using Engage.Application.Services.WebFileCategories.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebFileCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<WebFileCategoryDto>>> DtoList([FromQuery] WebFileCategoryListQuery query, CancellationToken cancellationToken)
    {

        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<WebFileCategoryDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<WebFileCategoryOption>>> OptionList([FromQuery] int? webFileGroupId, CancellationToken cancellationToken)
    {
        var queryable = Queryable(webFileGroupId);

        var entities = await queryable.OrderBy(e => e.WebFileGroup.Name)
                                      .ThenBy(e => e.DisplayName)
                                      .ProjectTo<WebFileCategoryOption>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WebFileCategoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var queryable = Queryable().Include(e => e.WebFileGroup);

        var entity = await queryable.SingleOrDefaultAsync(e => e.WebFileCategoryId == id, cancellationToken);

        return entity == null ? NotFound() : Ok(Mapper.Map<WebFileCategoryVm>(entity));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(WebFileCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.WebFileCategoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(WebFileCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(command.Id));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var result = await Mediator.Send(new WebFileCategoryDeleteCommand(id));

        return result == null ? NotFound() : Ok(result);
    }


    private IQueryable<WebFileCategory> Queryable(int? webFileGroupId = default)
    {
        var queryable = Context.WebFileCategories.AsQueryable().AsNoTracking();

        if (webFileGroupId.HasValue)
        {
            queryable = queryable.Where(e => e.WebFileGroupId == webFileGroupId);
        }

        return queryable;
    }

    private Dictionary<string, PaginationProperty> CreatePaginationModels()
    {
        return new Dictionary<string, PaginationProperty> {
            { "id", new PaginationProperty("WebFileCategoryId") },
            { "webFileGroupName", new PaginationProperty("WebFileGroupId", sortProperty: "WebFileGroup.Name") },
            { "name", new PaginationProperty("Name") },
            { "displayName", new PaginationProperty("DisplayName") },
            { "order", new PaginationProperty("Order") }
        };
    }
}