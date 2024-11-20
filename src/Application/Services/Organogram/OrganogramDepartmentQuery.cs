using Engage.Domain.Entities;

namespace Engage.Application.Services.Organogram;

public class OrganogramDepartmentQuery : IRequest<OrganogramTreeNodeDto>
{
    public int DivisionId { get; set; }
    public int? DepartmentGroupId { get; set; }
    public int? DepartmentId { get; set; }
    public int? EmployeeId { get; set; }
}
public record OrganogramDepartmentQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganogramDepartmentQuery, OrganogramTreeNodeDto>
{
    public async Task<OrganogramTreeNodeDto> Handle(OrganogramDepartmentQuery query, CancellationToken cancellationToken)
    {
        var division = await Context.EmployeeDivisions.FindAsync(query.DivisionId);

        if (division == null)
        {
            throw new NotFoundException(nameof(EmployeeDivision), query.DivisionId);
        }

        var departmentGroups = await Context.EngageDepartmentGroups
            .Where(d => (!query.DepartmentGroupId.HasValue || d.Id == query.DepartmentGroupId)
                     && d.Disabled == false
                     && d.Deleted == false)
            .Include(l => l.EngageDepartments.Where(dept => (!query.DepartmentId.HasValue || dept.Id == query.DepartmentId.Value)
                                                         && dept.Disabled == false
                                                         && dept.Deleted == false))
                .ThenInclude(m => m.Employees.Where(emp => (!query.EmployeeId.HasValue || emp.EmployeeId == query.EmployeeId.Value)
                                                        && emp.Employee.EmployeeDivisions.Any(ed => ed.EmployeeDivisionId == query.DivisionId)
                                                        && emp.Employee.Disabled == false
                                                        && emp.Employee.Deleted == false))
                    .ThenInclude(n => n.Employee)
                        .ThenInclude(n => n.EmployeeDivisions)
            .Include(dg => dg.EngageDepartments)
                .ThenInclude(d => d.Employees)
                    .ThenInclude(x => x.Employee)
                        .ThenInclude(e => e.EmployeeJobTitles)
                            .ThenInclude(ejt => ejt.EmployeeJobTitle)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var departmentGroupNodes = departmentGroups
        .Where(dg => (!query.DepartmentId.HasValue || !dg.EngageDepartments.IsNullOrEmpty())
                  && (!query.EmployeeId.HasValue || dg.EngageDepartments.Any(ed => ed.Employees.Any(e => e.EmployeeId == query.EmployeeId))))
        .Select(departmentGroup =>
        {
            var departmentNodes = departmentGroup.EngageDepartments
            .Where(department => !query.EmployeeId.HasValue || department.Employees.Any(e => e.EmployeeId == query.EmployeeId))
            .Select(department =>
            {
                var employees = department.Employees;

                var nodeIdPrefix = "departmentGroup" + departmentGroup.Id + "department" + department.Id;

                var jobTitleNodes = employees
                    .SelectMany(e => e.Employee.EmployeeJobTitles)
                    .GroupBy(ejt => ejt.EmployeeJobTitleId)
                    .Select(g => new OrganogramTreeNodeDto
                    {
                        Id = nodeIdPrefix + "jobTitle" + g.Key,
                        Title = g.First().EmployeeJobTitle.Name,
                        EntityId = g.Key,
                        EntityParentId = department.Id,
                        Children = g.Select(ejt => BuildEmployeeNode(ejt.Employee, nodeIdPrefix + "employee" + ejt.Employee.EmployeeId))
                                    .OrderBy(x => x.Title)
                                    .ToList()
                    })
                    .OrderBy(x => x.Title)
                    .ToList();

                return new OrganogramTreeNodeDto
                {
                    Id = nodeIdPrefix,
                    Title = department.Name,
                    EntityId = department.Id,
                    EntityParentId = department.EngageDepartmentGroupId,
                    Children = jobTitleNodes
                };
            })
            .OrderBy(x => x.Title)
            .ToList();

            return new OrganogramTreeNodeDto
            {
                Id = "departmentGroup" + departmentGroup.Id,
                Title = departmentGroup.Name,
                EntityId = departmentGroup.Id,
                Children = departmentNodes
            };
        })
        .OrderBy(x => x.Title)
        .ToList();

        return new OrganogramTreeNodeDto
        {
            Id = "division" + division.EmployeeDivisionId,
            Title = division.Name,
            EntityId = division.EmployeeDivisionId,
            Children = departmentGroupNodes
        };
    }

    private OrganogramTreeNodeDto BuildEmployeeNode(Employee employee, string nodeId)
    {
        var hasDifferentManager = employee.ManagerId != null && employee.ManagerId != employee.EmployeeId; //if the employee has a manager and the manager is not the employee itself

        return new OrganogramTreeNodeDto
        {
            Id = nodeId,
            Title = string.Join(" ", [employee.FirstName, employee.LastName]),
            SubTitle = employee.Code,
            EntityId = employee.EmployeeId,
            EntityParentId = hasDifferentManager ? employee.ManagerId : null,
            LabelValues = new Dictionary<string, string>
            {
                { "Job Title", employee.EmployeeJobTitles?.Select(s => s?.EmployeeJobTitle?.Name).ToList().StringJoin(", ") },
                //{ "First Name", employee.FirstName},
            },
            Config = new OrganogramTreeNodeConfig
            {
                SummaryNode = new BaseOrganogramTreeNodeConfig
                {
                    LabelValueConfig = new BaseOrganogramLabelValueConfig
                    {
                        IsLabelsHidden = true,
                        IsLabelValuesHidden = false,
                        //ExcludedLabels = new List<string> { "First Name" }
                    },
                },
                SubTitleConfig = new OrganogramSubTitleConfig
                {
                    Variant = OrganogramTitleVariant.Variant2
                }
            },
            Children = new List<OrganogramTreeNodeDto>()
        };
    }

}

