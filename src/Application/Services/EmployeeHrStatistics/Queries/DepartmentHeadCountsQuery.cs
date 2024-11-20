using Engage.Application.Services.EmployeeHrStatistics.Model;

namespace Engage.Application.Services.EmployeeHrStatistic.Queries;

public class DepartmentHeadCountsQuery: IRequest<DepartmentGroupHeadCountDto>
{
    public int? RegionId { get; set; }
}

public record DepartmentHeadCountsQueryHandler(IAppDbContext Context, IMapper Mapper): IRequestHandler<DepartmentHeadCountsQuery, DepartmentGroupHeadCountDto>
{
    public async Task<DepartmentGroupHeadCountDto> Handle(DepartmentHeadCountsQuery query, CancellationToken cancellationToken)
    {
        var departmentGroupEmployees = await Context.EngageDepartmentGroups.Where(depg => !depg.Disabled && !depg.Deleted)
                                                                           .Include(dg => dg.EngageDepartments.Where(d => !d.Disabled && !d.Deleted))
                                                                               .ThenInclude(d => d.Employees.Where(ed => !ed.Employee.Disabled && !ed.Employee.Deleted
                                                                                                                      && (!query.RegionId.HasValue 
                                                                                                                         ||  ed.Employee.EmployeeRegions.Any(er => er.EngageRegionId == query.RegionId.Value))))
                                                                           .AsNoTracking()
                                                                           .ToListAsync(cancellationToken);

        var departmentGroupHeadcount = departmentGroupEmployees.Select(dg => new StatCardDto
        {
            Label = dg.Name,
            Value = dg.EngageDepartments.SelectMany(d => d.Employees).Count()
        }).ToList();

        var employeesInASingleDepartmentGroup = departmentGroupEmployees
            .SelectMany(dg => dg.EngageDepartments.SelectMany(d => d.Employees))
            .GroupBy(e => e.EmployeeId)
            .Where(group => group.Select(e => e.EngageDepartmentId).Distinct().Count() == 1)
            .Select(group => group.Key)
            .Count();

        var employeesInMultipleDepartmentGroups = departmentGroupEmployees
            .SelectMany(dg => dg.EngageDepartments.SelectMany(d => d.Employees))
            .GroupBy(e => e.EmployeeId)
            .Where(group => group.Select(e => e.EngageDepartmentId).Distinct().Count() > 1)
            .Select(group => group.Key)
            .Count();

        var dedicatedDepartmentGroupHeadcount = new DedicatedDepartmentGroupHeadCountDto
        {
            SingleRoleHeadCount = new StatCardDto
            {
                Label = "Dedicated Roles",
                Value = employeesInASingleDepartmentGroup
            },
            MultiRoleHeadCount = new StatCardDto
            {
                Label = "Dual Roles",
                Value = employeesInMultipleDepartmentGroups
            }
        };

        var departmentGroupHeadcountDto = new DepartmentGroupHeadCountDto
        {
            DedicatedDepartmentGroupEmployees = dedicatedDepartmentGroupHeadcount,
            DepartmentGroupHeadCounts = departmentGroupHeadcount
        };

        return departmentGroupHeadcountDto;
    }
}
