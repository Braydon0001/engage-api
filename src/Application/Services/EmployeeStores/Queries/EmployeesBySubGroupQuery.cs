using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.EmployeeStores.Queries;

public class EmployeesBySubGroupQuery : GetQuery, IRequest<ListResult<EmployeeListDto>>
{
    public int EngageSubGroupId { get; set; }
}

public class EmployeesBySubGroupQueryHandler : BaseListQueryHandler, IRequestHandler<EmployeesBySubGroupQuery, ListResult<EmployeeListDto>>
{
    public EmployeesBySubGroupQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeListDto>> Handle(EmployeesBySubGroupQuery query, CancellationToken cancellationToken)
    {
        var employeeIds = await _context.EmployeeStores.Where(e => e.EngageSubGroupId == query.EngageSubGroupId)
                                                    .Join(_context.Employees,
                                                          employeeStore => employeeStore.EmployeeId,
                                                          employee => employee.EmployeeId,
                                                          (employeeStore, employee) => employee.EmployeeId)
                                                    .Distinct()
                                                    .ToListAsync(cancellationToken);

        var employees = await _context.Employees.Where(e => employeeIds.Contains(e.EmployeeId))
                                                .OrderBy(e => e.Code)
                                                .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                                                .ToListAsync(cancellationToken);

        return new ListResult<EmployeeListDto>(employees);
    }
}
