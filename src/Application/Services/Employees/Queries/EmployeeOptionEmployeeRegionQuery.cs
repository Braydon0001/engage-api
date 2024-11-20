using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeeOptionEmployeeRegionQuery : GetQuery, IRequest<List<OptionDto>>
{
}
public class EmployeeOptionEmployeeRegionHandler : BaseQueryHandler, IRequestHandler<EmployeeOptionEmployeeRegionQuery, List<OptionDto>>
{
    private readonly IMediator _mediator;
    public EmployeeOptionEmployeeRegionHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<OptionDto>> Handle(EmployeeOptionEmployeeRegionQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                          .AsNoTracking()
                                          .AsQueryable();

        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        queryable = queryable.Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                                                employee => employee.EmployeeId,
                                                                region => region.EmployeeId,
                                                                (employee, region) => employee);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.FirstName, $"%{request.Search}%"))
                                            || (EF.Functions.Like(e.LastName, $"%{request.Search}%"))
                                            || (EF.Functions.Like(e.Code, $"%{request.Search}%"))
                                            );
        }

        var entities = await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.EmployeeId, Name = e.FirstName + " " + e.LastName + " - " + e.Code })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);

        return entities.DistinctBy(e => e.Id).ToList();
    }
}