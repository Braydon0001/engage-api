using Engage.Application.Services.ClaimYears.Models;

namespace Engage.Application.Services.ClaimYears.Queries;

public class ClaimYearsQuery : GetQuery, IRequest<ListResult<ClaimYearDto>>
{
}

public class ClaimYearsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimYearsQuery, ListResult<ClaimYearDto>>
{
    public ClaimYearsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<ClaimYearDto>> Handle(ClaimYearsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimYears.OrderBy(e => e.ClaimYearId)
                                                .ProjectTo<ClaimYearDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<ClaimYearDto>(entities);
    }
}
