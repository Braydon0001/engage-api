using Engage.Application.Services.BudgetYears.Models;

namespace Engage.Application.Services.BudgetYears.Queries
{
    public class BudgetYearVmQuery : GetByIdQuery, IRequest<BudgetYearVm>
    {

    }

    public class GetBudgetYearQueryHandler : BaseQueryHandler, IRequestHandler<BudgetYearVmQuery, BudgetYearVm>
    {
        public GetBudgetYearQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<BudgetYearVm> Handle(BudgetYearVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.BudgetYears.SingleAsync(x => x.BudgetYearId == request.Id, cancellationToken);

            return _mapper.Map<BudgetYear, BudgetYearVm>(entity);
        }
    }
}
