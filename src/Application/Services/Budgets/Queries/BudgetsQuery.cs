using Engage.Application.Services.Budgets.Models;

namespace Engage.Application.Services.Budgets.Queries;

public class BudgetsQuery : GetQuery, IRequest<ListResult<BudgetListDto>>
{

}

public class BudgetsQueryHandler : BaseQueryHandler, IRequestHandler<BudgetsQuery, ListResult<BudgetListDto>>
{
    public BudgetsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<BudgetListDto>> Handle(BudgetsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Budgets.ProjectTo<BudgetListDto>(_mapper.ConfigurationProvider)
                                             .ToListAsync(cancellationToken);

        return new ListResult<BudgetListDto>(entities);
    }
}
