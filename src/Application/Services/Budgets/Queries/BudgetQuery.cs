using Engage.Application.Services.Budgets.Models;
namespace Engage.Application.Services.Budgets.Queries;

public class BudgetQuery : GetByIdQuery, IRequest<BudgetDto>
{
}

public class BudgetQueryHandler : BaseQueryHandler, IRequestHandler<BudgetQuery, BudgetDto>
{
    public BudgetQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<BudgetDto> Handle(BudgetQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Budgets.SingleAsync(x => x.BudgetId == request.Id, cancellationToken);

        return _mapper.Map<Budget, BudgetDto>(entity);
    }
}
