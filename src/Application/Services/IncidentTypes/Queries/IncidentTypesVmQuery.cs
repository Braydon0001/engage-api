using Engage.Application.Services.IncidentTypes.Models;

namespace Engage.Application.Services.IncidentTypes.Queries;

public class IncidentTypeVmQuery : IRequest<IncidentTypeVm>
{
    public int Id { get; set; }
}

public class IncidentTypeVmQueryHandler : BaseQueryHandler, IRequestHandler<IncidentTypeVmQuery, IncidentTypeVm>
{
    public IncidentTypeVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IncidentTypeVm> Handle(IncidentTypeVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentTypes.SingleAsync(e => e.IncidentTypeId == request.Id, cancellationToken);

        return _mapper.Map<IncidentType, IncidentTypeVm>(entity);
    }
}
