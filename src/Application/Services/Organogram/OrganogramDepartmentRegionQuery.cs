namespace Engage.Application.Services.Organogram;

public class OrganogramDepartmentRegionQuery : IRequest<OrganogramTreeNodeDto>
{
    public int DivisionId { get; set; }
    public int? RegionId { get; set; }
    public int? SubRegionId { get; set; }
    public int? DepartmentGroupId { get; set; }
    public int? DepartmentId { get; set; }
    public int? EmployeeId { get; set; }
}
public record OrganogramDepartmentRegionQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganogramDepartmentRegionQuery, OrganogramTreeNodeDto>
{
    public async Task<OrganogramTreeNodeDto> Handle(OrganogramDepartmentRegionQuery query, CancellationToken cancellationToken)
    {
        var division = await Context.EmployeeDivisions.FindAsync(query.DivisionId);

        if (division == null)
        {
            throw new NotFoundException(nameof(EmployeeDivision), query.DivisionId);
        }

        EngageSubRegion subRegion = new();

        if (query.SubRegionId.HasValue)
        {
            subRegion = await Context.EngageSubRegions.FindAsync(query.SubRegionId.Value);
            if (subRegion == null)
            {
                throw new NotFoundException(nameof(EngageSubRegions), query.SubRegionId.Value);
            }
            query.RegionId = subRegion.EngageRegionId; // Override RegionId with the region linked to the subregion
        }

        var engageRegionQuery = Context.EngageRegions.AsQueryable();

        if (query.RegionId.HasValue)
        {
            engageRegionQuery = engageRegionQuery.Where(r => r.Id == query.RegionId);
        }

        var regions = await engageRegionQuery.ToListAsync(cancellationToken);

        var departmentGroups = await Context.EngageDepartmentGroups
                .Where(d => (!query.DepartmentGroupId.HasValue || d.Id == query.DepartmentGroupId)
                         && d.Disabled == false
                         && d.Deleted == false)
                .Include(l => l.EngageDepartments.Where(dept => (!query.DepartmentId.HasValue || dept.Id == query.DepartmentId.Value)
                                                             && dept.Disabled == false
                                                             && dept.Deleted == false))
                    .ThenInclude(m => m.Employees.Where(emp => (!query.EmployeeId.HasValue || emp.EmployeeId == query.EmployeeId.Value)
                                                            && (!query.RegionId.HasValue || emp.Employee.EmployeeRegions.Any(er => er.EngageRegionId == query.RegionId.Value))
                                                            && (!query.SubRegionId.HasValue || emp.Employee.EngageSubRegionId == query.SubRegionId)
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
                .Include(dg => dg.EngageDepartments)
                    .ThenInclude(d => d.Employees)
                        .ThenInclude(x => x.Employee)
                            .ThenInclude(e => e.EmployeeRegions)
                .Include(dg => dg.EngageDepartments)
                    .ThenInclude(d => d.Employees)
                        .ThenInclude(x => x.Employee)
                            .ThenInclude(e => e.EngageSubRegion)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        var departmentGroupNodes = departmentGroups
        .Where(dg => (!query.DepartmentId.HasValue || !dg.EngageDepartments.IsNullOrEmpty())
                  && (!query.EmployeeId.HasValue || dg.EngageDepartments.Any(ed => ed.Employees.Any(e => e.EmployeeId == query.EmployeeId)))
                  && (!query.RegionId.HasValue || dg.EngageDepartments.Any(ed => ed.Employees.Any(e => e.Employee.EmployeeRegions.Any(er => er.EngageRegionId == query.RegionId))))
              )
        .Select(departmentGroup =>
        {
            var departmentNodes = departmentGroup.EngageDepartments
            .Where(department => !query.EmployeeId.HasValue || department.Employees.Any(e => e.EmployeeId == query.EmployeeId))
            .Select(department =>
            {
                var regionNodes = regions
                .Where(region => department.Employees.Any(e => e.Employee.EmployeeRegions.Any(er => er.EngageRegionId == region.Id)))
                .Select(region =>
                {
                    var engageRegionEmployees = department.Employees
                        .Where(e => e.Employee.EmployeeRegions.Any(er => er.EngageRegionId == region.Id))
                        .ToList();

                    var nodeIdPrefix = "departmentGroup" + departmentGroup.Id + "department" + department.Id + "region" + region.Id;

                    List<OrganogramTreeNodeDto> regionChildren = new();
                    var jobTitleNodes = engageRegionEmployees
                        .SelectMany(e => e.Employee.EmployeeJobTitles)
                        .GroupBy(ejt => ejt.EmployeeJobTitleId)
                        .Select(g => new OrganogramTreeNodeDto
                        {
                            Id = nodeIdPrefix + "jobTitle" + g.Key,
                            Title = g.First().EmployeeJobTitle.Name,
                            EntityId = g.Key,
                            EntityParentId = department.Id,
                            Children = g.Select(ejt => BuildEmployeeNode(ejt.Employee, nodeIdPrefix + "employee"+ ejt.Employee.EmployeeId.ToString(), query))
                                        .OrderBy(x => x.Title)
                                        .ToList()
                        })
                        .OrderBy(x => x.Title)
                        .ToList();

                    List<OrganogramTreeNodeDto> regionNodeChildren = jobTitleNodes;

                    if (query.SubRegionId.HasValue)
                    {

                        var subRegionNode = new OrganogramTreeNodeDto
                        {
                            Id = "subRegion" + query.SubRegionId,
                            Title = subRegion.Name,
                            Children = jobTitleNodes
                        };

                        regionNodeChildren = [subRegionNode];
                    }

                    return new OrganogramTreeNodeDto
                    {
                        Id = nodeIdPrefix,
                        Title = region.Name,
                        EntityParentId = department.Id,
                        EntityId = region.Id,
                        Children = regionNodeChildren
                    };
                })
                .OrderBy(x => x.Title)
                .ToList();

                return new OrganogramTreeNodeDto
                {
                    Id = "departmentGroup" + departmentGroup.Id + "department" + department.Id,
                    Title = department.Name,
                    EntityId = department.Id,
                    EntityParentId = department.EngageDepartmentGroupId,
                    Children = regionNodes
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

    private OrganogramTreeNodeDto BuildEmployeeNode(Employee employee, string nodeId, OrganogramDepartmentRegionQuery query)
    {
        var hasDifferentManager = employee.ManagerId != null && employee.ManagerId != employee.EmployeeId; //if the employee has a manager and the manager is not the employee itself

        var labelValues = new Dictionary<string, string>
        {
            { "Job Title", employee.EmployeeJobTitles?.Select(s => s?.EmployeeJobTitle?.Name).ToList().StringJoin(", ") }
        };

        // Conditionally add "Sub Region" label
        if (!query.SubRegionId.HasValue)
        {
            labelValues.Add("Sub Region", employee?.EngageSubRegion?.Name ?? "Subregion Unassigned");
        }

        return new OrganogramTreeNodeDto
        {
            Id = nodeId,
            Title = string.Join(" ", [employee.FirstName, employee.LastName]),
            SubTitle = employee.Code,
            EntityId = employee.EmployeeId,
            EntityParentId = hasDifferentManager ? employee.ManagerId : null,
            LabelValues = labelValues,
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

