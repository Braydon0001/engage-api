using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeesToDisableQuery : GetQuery, IRequest<ListResult<EmployeeListDto>>
{
}

public class EmployeesToDisableQueryHandler : BaseQueryHandler, IRequestHandler<EmployeesToDisableQuery, ListResult<EmployeeListDto>>
{
    public EmployeesToDisableQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EmployeeListDto>> Handle(EmployeesToDisableQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Employees.Where(e => e.Disabled == false && e.EndDate.Value.Date < DateTime.Now.Date)
                                               .OrderBy(e => e.FirstName)
                                               .ThenBy(e => e.LastName)
                                               .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

        return new ListResult<EmployeeListDto>(entities);
    }
}
