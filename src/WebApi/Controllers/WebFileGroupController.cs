// auto-generated
using Engage.Application.Services.WebFileGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebFileGroupController : BaseController
{
    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<WebFileGroupOption>>> OptionList(CancellationToken cancellationToken)
    {
        var queryable = Queryable();

        var entities = await queryable.OrderBy(e => e.WebFileGroupId)
                                      .ProjectTo<WebFileGroupOption>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return Ok(entities);
    }

    private IQueryable<WebFileGroup> Queryable()
    {
        var queryable = Context.WebFileGroups.AsQueryable().AsNoTracking();
        
        return queryable; 
    }


}