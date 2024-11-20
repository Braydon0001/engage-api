using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Employees.Queries
{
    public class EmployeeOptionsQuery : GetQuery, IRequest<List<OptionDto>>
    {
    }

    public class EmployeeOptionsQueryHandler : IRequestHandler<EmployeeOptionsQuery, List<OptionDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly IUserService _user;

        public EmployeeOptionsQueryHandler(IAppDbContext context, IMediator mediator, IUserService user)
        {
            _context = context;
            _mediator = mediator;
            _user = user;
        }

        public async Task<List<OptionDto>> Handle(EmployeeOptionsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                              .AsQueryable();

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

            return await queryable.Where(e => e.Disabled == false)
                                  .Select(e => new OptionDto { Id = e.EmployeeId, Name = e.FirstName + " " + e.LastName + " - " + e.Code })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
        }
    }
}
