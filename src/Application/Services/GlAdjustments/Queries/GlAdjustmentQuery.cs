using Engage.Application.Services.GlAdjustments.Models;

namespace Engage.Application.Services.GlAdjustments.Queries;

public class GlAdjustmentQuery : GetByIdQuery, IRequest<GLAdjustmentDto>
{
}

public class GlAdjustmentQueryHandler : BaseQueryHandler, IRequestHandler<GlAdjustmentQuery, GLAdjustmentDto>
{
    public GlAdjustmentQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<GLAdjustmentDto> Handle(GlAdjustmentQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.GLAdjustments.SingleAsync(x => x.GLAdjustmentId == request.Id, cancellationToken);
        return _mapper.Map<GLAdjustment, GLAdjustmentDto>(entity);
    }
}
