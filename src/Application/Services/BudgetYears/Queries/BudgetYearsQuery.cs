using Engage.Application.Services.BudgetYears.Models;

namespace Engage.Application.Services.BudgetYears.Queries
{
    public class BudgetYearsQuery : GetQuery, IRequest<ListResult<BudgetYearDto>>
    {
    }

    public class BudgetYearsQueryHandler : BaseQueryHandler, IRequestHandler<BudgetYearsQuery, ListResult<BudgetYearDto>>
    {
        public BudgetYearsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<BudgetYearDto>> Handle(BudgetYearsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.BudgetYears.OrderBy(e => e.Name)
                                                     .ProjectTo<BudgetYearDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);

            return new ListResult<BudgetYearDto>(entities);
        }
    }
}
