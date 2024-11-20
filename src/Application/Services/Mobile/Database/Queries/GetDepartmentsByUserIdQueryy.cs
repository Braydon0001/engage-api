using Engage.Application.Services.Mobile.Database.Models;

namespace Engage.Application.Services.Mobile.Database.Queries;

public class GetDepartmentsByUserIdQuery : IRequest<List<DepartmentDto>>
{
    public int EmployeeId { get; set; }
}

public class GetDepartmentsByUserIdQueryHandler : BaseQueryHandler, IRequestHandler<GetDepartmentsByUserIdQuery, List<DepartmentDto>>
{
    public GetDepartmentsByUserIdQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var sql = @"SELECT * 
                    FROM opt_engagedepartments engDept
                        LEFT JOIN EmployeeDepartments empDept
                            on engDept.Id = empDept.EngageDepartmentId
                    WHERE engDept.Disabled = 0
                    AND engDept.Deleted = 0
                    AND empDept.EmployeeId = " + request.EmployeeId.ToString();

        return await _context.EngageDepartments
                    .FromSqlRaw(sql)
                    .OrderBy(o => o.Name)
                    .Select(d => new DepartmentDto
                    {
                        Id = d.Id,
                        Name = d.Name
                    }).ToListAsync();

    }
}
