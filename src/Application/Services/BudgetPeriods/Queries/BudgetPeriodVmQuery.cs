using Engage.Application.Services.BudgetPeriods.Models;

namespace Engage.Application.Services.BudgetPeriods.Queries;

public class BudgetPeriodVmQuery : GetByIdQuery, IRequest<BudgetPeriodVm>
{
}

public class BudgetPeriodQueryHandler : BaseQueryHandler, IRequestHandler<BudgetPeriodVmQuery, BudgetPeriodVm>
{
    public BudgetPeriodQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<BudgetPeriodVm> Handle(BudgetPeriodVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.BudgetPeriods.Include(e => e.BudgetYear)
                                                 .SingleAsync(x => x.BudgetPeriodId == request.Id, cancellationToken);

        return _mapper.Map<BudgetPeriod, BudgetPeriodVm>(entity);
    }
}
