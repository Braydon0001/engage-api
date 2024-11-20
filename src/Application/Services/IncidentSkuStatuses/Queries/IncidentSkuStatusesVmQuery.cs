using Engage.Application.Services.IncidentSkuStatuses.Models;

namespace Engage.Application.Services.IncidentSkuStatuses.Queries;

public class IncidentSkuStatusVmQuery : IRequest<IncidentSkuStatusVm>
{
    public int Id { get; set; }
}

public class IncidentSkuStatusVmQueryHandler : BaseQueryHandler, IRequestHandler<IncidentSkuStatusVmQuery, IncidentSkuStatusVm>
{
    public IncidentSkuStatusVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IncidentSkuStatusVm> Handle(IncidentSkuStatusVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentSkuStatuses.SingleAsync(e => e.IncidentSkuStatusId == request.Id, cancellationToken);

        return _mapper.Map<IncidentSkuStatus, IncidentSkuStatusVm>(entity);
    }
}
