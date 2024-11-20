using Engage.Application.Services.Incidents.Models;

namespace Engage.Application.Services.Incidents.Queries;

public class IncidentVmQuery : IRequest<IncidentVm>
{
    public int Id { get; set; }
}

public class IncidentVmQueryHandler : BaseQueryHandler, IRequestHandler<IncidentVmQuery, IncidentVm>
{
    public IncidentVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IncidentVm> Handle(IncidentVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Incidents.Include(e => e.ClientType)
                                             .Include(e => e.IncidentType)
                                             .Include(e => e.IncidentStatus)
                                             .Include(e => e.Supplier)
                                             .Include(e => e.Store)
                                             .SingleAsync(e => e.IncidentId == request.Id, cancellationToken);

        return _mapper.Map<Incident, IncidentVm>(entity);
    }
}
