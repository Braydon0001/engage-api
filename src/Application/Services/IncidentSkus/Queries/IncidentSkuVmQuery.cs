using Engage.Application.Services.IncidentSkus.Models;

namespace Engage.Application.Services.IncidentSkus.Queries;

public class IncidentSkuVmQuery : IRequest<IncidentSkuVm>
{
    public int Id { get; set; }
}

public class IncidentSkuVmQueryHandler : BaseQueryHandler, IRequestHandler<IncidentSkuVmQuery, IncidentSkuVm>
{
    public IncidentSkuVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IncidentSkuVm> Handle(IncidentSkuVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.IncidentSkus.Include(e => e.IncidentSkuType)
                                                .Include(e => e.IncidentSkuStatus)
                                                .Include(e => e.DCProduct)
                                                .SingleAsync(e => e.IncidentSkuId == request.Id, cancellationToken);

        return _mapper.Map<IncidentSku, IncidentSkuVm>(entity);
    }
}
