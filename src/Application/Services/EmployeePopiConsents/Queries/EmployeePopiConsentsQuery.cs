using Engage.Application.Services.EmployeePopiConsents.Models;

namespace Engage.Application.Services.EmployeePopiConsents.Queries;

public class EmployeePopiConsentsQuery : IRequest<ListResult<EmployeePopiConsentDto>>
{
    public int? EmployeeId { get; set; }
}

public class GetEmployeePopiConsentsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeePopiConsentsQuery, ListResult<EmployeePopiConsentDto>>
{
    public GetEmployeePopiConsentsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeePopiConsentDto>> Handle(EmployeePopiConsentsQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeePopiConsents.AsQueryable();

        if (query.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == query.EmployeeId);
        }

        var entities = await queryable.Include(e => e.Employee)
                                      .ThenInclude(e => e.EmployeeRegions)
                                      .ThenInclude(e => e.EngageRegion)
                                      .OrderByDescending(e => e.EmployeePopiConsentId)
                                      .ProjectTo<EmployeePopiConsentDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeePopiConsentDto>(entities);
    }
}
