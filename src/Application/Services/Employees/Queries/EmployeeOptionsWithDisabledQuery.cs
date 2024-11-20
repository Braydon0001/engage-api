using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Employees.Queries
{
    public class EmployeeOptionsWithDisabledQuery : GetQuery, IRequest<List<OptionDto>>
    {
    }

    public class EmployeeOptionsWithDisabledQueryHandler : IRequestHandler<EmployeeOptionsWithDisabledQuery, List<OptionDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;

        public EmployeeOptionsWithDisabledQueryHandler(IAppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<List<OptionDto>> Handle(EmployeeOptionsWithDisabledQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Employees.AsQueryable();

            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

            if (!engageRegionIds.Contains(7))
            {
                queryable = queryable
                                    .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                          employee => employee.EmployeeId,
                                          region => region.EmployeeId,
                                          (employee, region) => employee);
            }

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                queryable = queryable.Where(e => (EF.Functions.Like(e.FirstName, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.LastName, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.Code, $"%{request.Search}%"))
                                                );
            }

            return await queryable.IgnoreQueryFilters()
                                  .Where(e => e.Deleted == false)
                                  .Select(e => new OptionDto { Id = e.EmployeeId, Name = e.FirstName + " " + e.LastName + " - " + e.Code + (e.Disabled ? " - DISABLED" : "") })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
        }
    }
}
