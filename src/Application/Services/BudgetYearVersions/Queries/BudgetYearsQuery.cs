using Engage.Application.Services.BudgetYearVersions.Models;

namespace Engage.Application.Services.BudgetYearVersions.Queries;

public class BudgetYearsQuery : GetQuery, IRequest<ListResult<BudgetYearVersionDto>>
{

}

public class BudgetYearVersionsQueryHandler : BaseQueryHandler, IRequestHandler<BudgetYearsQuery, ListResult<BudgetYearVersionDto>>
{
    public BudgetYearVersionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<BudgetYearVersionDto>> Handle(BudgetYearsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.BudgetYearVersions.OrderBy(e => e.BudgetYear.Name)
                                                        .ThenBy(e => e.BudgetVersion.Id)
                                                        .ProjectTo<BudgetYearVersionDto>(_mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);

        return new ListResult<BudgetYearVersionDto>(entities);
    }
}
