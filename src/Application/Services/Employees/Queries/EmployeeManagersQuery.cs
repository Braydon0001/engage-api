using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeeManagersQuery : GetQuery, IRequest<ListResult<EmployeeListDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeManagersQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeManagersQuery, ListResult<EmployeeListDto>>
{
    public EmployeeManagersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EmployeeListDto>> Handle(EmployeeManagersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Employees.Where(e => e.EmployeeId != request.EmployeeId && e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                               .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

        return new ListResult<EmployeeListDto>(entities);
    }
}
