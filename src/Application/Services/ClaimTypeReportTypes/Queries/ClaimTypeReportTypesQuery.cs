using Engage.Application.Services.ClaimTypeReportTypes.Models;

namespace Engage.Application.Services.ClaimTypeReportTypes.Queries;

public class ClaimTypeReportTypesQuery : GetQuery, IRequest<ListResult<ClaimTypeReportTypeDto>>
{
}

public class ClaimTypeReportTypesQueryHandler : BaseQueryHandler, IRequestHandler<ClaimTypeReportTypesQuery, ListResult<ClaimTypeReportTypeDto>>
{
    public ClaimTypeReportTypesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ClaimTypeReportTypeDto>> Handle(ClaimTypeReportTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimTypeReportTypes
                                                .OrderBy(e => e.ClaimReportType.Name)
                                                .ThenBy(e=> e.ClaimType.Name)
                                                .ProjectTo<ClaimTypeReportTypeDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<ClaimTypeReportTypeDto>
        {
            Count = entities.Count,
            Data = entities
        };

    }
}