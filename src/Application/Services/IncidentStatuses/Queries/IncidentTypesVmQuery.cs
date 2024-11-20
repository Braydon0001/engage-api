using Engage.Application.Services.IncidentStatuses.Models;

namespace Engage.Application.Services.IncidentStatuses.Queries;

public class IncidentStatusVmQuery : IRequest<IncidentStatusVm>
{
    public int Id { get; set; }
}

public class IncidentStatusVmQueryHandler : BaseQueryHandler, IRequestHandler<IncidentStatusVmQuery, IncidentStatusVm>
{
    public IncidentStatusVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IncidentStatusVm> Handle(IncidentStatusVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentStatuses.SingleAsync(e => e.IncidentStatusId == request.Id, cancellationToken);

        return _mapper.Map<IncidentStatus, IncidentStatusVm>(entity);
    }
}
