using Engage.Application.Services.EmployeeWeb.Models;
using Engage.Application.Services.WebEvents.Queries;

namespace Engage.Application.Services.EmployeeWeb.Queries;

public record EmployeeWebQuery(int EmployeeId) : IRequest<EmployeeWebVm>
{
}

public class EmployeeWebHandler : BaseQueryHandler, IRequestHandler<EmployeeWebQuery, EmployeeWebVm>
{
    readonly IMediator _mediator;
    public EmployeeWebHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<EmployeeWebVm> Handle(EmployeeWebQuery request, CancellationToken cancellationToken)
    {
        var kpis = await _context.EmployeeEmployeeKpis.Where(e => e.EmployeeId == request.EmployeeId).Select(o => new KpiCard()
        {
            Heading = o.EmployeeKpi.Name,
            Score = o.Score,
            KpiTierId = o.EmployeeKpiTierId
        }).ToListAsync(cancellationToken);

        kpis.AddRange(await _context.EmployeeStoreKpis.Where(e => e.EmployeeId == request.EmployeeId).Select(o => new KpiCard()
        {
            Heading = o.EmployeeKpi.Name,
            Score = o.Score,
            KpiTierId = o.EmployeeKpiTierId
        }).ToListAsync(cancellationToken));

        var notifications = await _mediator.Send(new WebEventActiveQuery(), cancellationToken);

        return new EmployeeWebVm()
        {
            KpiCards = kpis,
            Notifications = notifications
        };
    }
}