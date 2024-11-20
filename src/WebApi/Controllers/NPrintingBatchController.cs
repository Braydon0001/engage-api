// auto-generated
using Engage.Application.Services.NPrintingBatches.Queries;

namespace Engage.WebApi.Controllers;

public partial class NPrintingBatchController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<NPrintingBatchDto>>> DtoList(CancellationToken cancellationToken)
    {
        var queryable = Queryable();

        var entities = await queryable.OrderByDescending(e => e.NPrintingBatchId)
                                      .ProjectTo<NPrintingBatchDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return Ok(new ListResult<NPrintingBatchDto>(entities));
    }

    private IQueryable<NPrintingBatch> Queryable()
    {
        var queryable = Context.NPrintingBatches.AsQueryable().AsNoTracking();

        return queryable;
    }


}