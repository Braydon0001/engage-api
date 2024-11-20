using Engage.Application.Services.IncidentSkuTypes.Models;

namespace Engage.Application.Services.IncidentSkuTypes.Queries;

public class IncidentSkuTypeVmQuery : IRequest<IncidentSkuTypeVm>
{
    public int Id { get; set; }
}

public class IncidentSkuTypeVmQueryHandler : BaseQueryHandler, IRequestHandler<IncidentSkuTypeVmQuery, IncidentSkuTypeVm>
{
    public IncidentSkuTypeVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IncidentSkuTypeVm> Handle(IncidentSkuTypeVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentSkuTypes.SingleAsync(e => e.IncidentSkuTypeId == request.Id, cancellationToken);

        return _mapper.Map<IncidentSkuType, IncidentSkuTypeVm>(entity);
    }
}
