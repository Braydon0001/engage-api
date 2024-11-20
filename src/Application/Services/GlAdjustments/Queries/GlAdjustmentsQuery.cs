using Engage.Application.Services.GLAdjustments.Models;

namespace Engage.Application.Services.GlAdjustments.Queries;

public class GlAdjustmentsQuery : GetQuery, IRequest<ListResult<GlAdjustmentListDto>>
{

}

public class GlAdjustmentsQueryHandler : BaseQueryHandler, IRequestHandler<GlAdjustmentsQuery, ListResult<GlAdjustmentListDto>>
{
    public GlAdjustmentsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<GlAdjustmentListDto>> Handle(GlAdjustmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.GLAdjustments.OrderBy(e => e.GLAdjustmentId)
                                                   .ProjectTo<GlAdjustmentListDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<GlAdjustmentListDto>(entities);
    }
}
