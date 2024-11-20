using Engage.Domain.Entities;

namespace Engage.Application.Services.Organogram;

public class OrganogramManagerJobTitleQuery : IRequest<OrganogramTreeNodeDto>
{
    public int DivisionId { get; set; }
    public int? EmployeeId { get; set; }
}
public record OrganogramManagerJobTitleQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganogramManagerJobTitleQuery, OrganogramTreeNodeDto>
{
    public async Task<OrganogramTreeNodeDto> Handle(OrganogramManagerJobTitleQuery query, CancellationToken cancellationToken)
    {
        var division = await Context.EmployeeDivisions.FindAsync(query.DivisionId);

        if (division == null)
        {
            throw new NotFoundException(nameof(EmployeeDivision), query.DivisionId);
        }

        var engageEmployeesQuery = Context.Employees.AsQueryable()
                                                    .IgnoreQueryFilters() //ignore the global query filter for disabled employees
                                                    .Where(e => e.EmployeeDivisions.Any(x => x.EmployeeDivisionId == query.DivisionId
                                                                                                      //&& x.Employee.Disabled == false
                                                                                                        && x.Employee.Deleted == false));

        var engageEmployees = await engageEmployeesQuery.Include(z => z.EmployeeJobTitles)
                                                           .ThenInclude(l => l.EmployeeJobTitle)
                                                        .Include(y => y.EmployeeRegions)
                                                           .ThenInclude(x => x.EngageRegion)
                                                        .Include(p => p.EmployeeDivisions)
                                                          .Include(f => f.EmployeeDepartments)
                                                               .ThenInclude(g => g.EngageDepartment)
                                                                .ThenInclude(h => h.EngageDepartmentGroup)
                                                        .AsNoTracking()
                                                        .ToListAsync(cancellationToken);

        var employeeNodes = engageEmployees.ToDictionary(e => e.EmployeeId, e => BuildEmployeeNode(e));

        foreach (var employeeNode in employeeNodes.Values)
        {

            var employeeId = employeeNode.EntityId;
            var employee = engageEmployees.First(e => e.EmployeeId == employeeId);
            var hasDifferentManager = employee.ManagerId != null && employee.ManagerId != employee.EmployeeId; //if the employee has a manager and the manager is not the employee itself
            if (hasDifferentManager && employeeNodes.ContainsKey(employee.ManagerId.Value))
            {
                employeeNodes[employee.ManagerId.Value].Children.Add(employeeNode);
                employeeNodes[employee.ManagerId.Value].Children = employeeNodes[employee.ManagerId.Value].Children.OrderBy(x => x.Title).ToList();
            }
        }

        var rootNodes = employeeNodes.Values.Where(n => n.EntityParentId == null).ToList(); //any without a manager id including the hierarchy top node

        // Filter the tree to exclude disabled employees without subordinates
        rootNodes = FilterTree(rootNodes);

        var mainEmployeeNodes = rootNodes.Where(e => !e.Children.IsNullOrEmpty()).ToList();

        if (query.EmployeeId.HasValue)
        {
            var employeeNode = employeeNodes[query.EmployeeId.Value];

            if (employeeNode != null)
            {
                var managerNode = employeeNode;
                var rootManagerNode = new OrganogramTreeNodeDto
                {
                    Id = "division" + division.EmployeeDivisionId,
                    EntityId = division.EmployeeDivisionId,
                    Title = division.Name,
                    Children = [managerNode]
                };

                TraverseAndGroupByJobTitle(rootManagerNode, 1, 4);

                return rootManagerNode;
            }

        }

        var unassignedEmployeeNodes = new OrganogramTreeNodeDto
        {
            Title = "Unassigned",
            Children = rootNodes.Where(e => e.Children.IsNullOrEmpty()).ToList()
        };

        if (!unassignedEmployeeNodes.Children.IsNullOrEmpty())
        {
            mainEmployeeNodes.Add(unassignedEmployeeNodes);
        }

        var rootNode = new OrganogramTreeNodeDto
        {
            Id = "division" + division.EmployeeDivisionId,
            EntityId = division.EmployeeDivisionId,
            Title = division.Name,
            Children = mainEmployeeNodes
        };

        TraverseAndGroupByJobTitle(rootNode, 1, 4);

        return rootNode;
    }

    private void TraverseAndGroupByJobTitle(OrganogramTreeNodeDto node, int currentLevel, int targetLevel)
    {
        if (node == null) return;

        // When one level above the target, start grouping the next level's children by job title
        if (currentLevel == targetLevel - 1)
        {
            var allDescendants = new List<OrganogramTreeNodeDto>();
            CollectAllDescendants(node, allDescendants);
            allDescendants = allDescendants.OrderBy(x => x.Title)
                                           .Where(y => !IsDisabled(y)) // excluding disabled employees when grouped by job title / not hierarchical
                                           .ToList();

            var groupedByJobTitle = allDescendants
                .GroupBy(descendant => descendant.LabelValues.ContainsKey("Job Title") ? descendant.LabelValues["Job Title"] : "Unassigned")
                .OrderBy(group => group.Key)
                .Select(group => new OrganogramTreeNodeDto
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = group.Key, // Use job title as the title of the new node
                    Children = group.ToList() // All descendants under this job title
                }).ToList();

            // Replace the node's children with the grouped list
            node.Children = groupedByJobTitle;
        }
        else
        {
            // Continue traversing deeper into the hierarchy
            foreach (var child in node.Children)
            {
                TraverseAndGroupByJobTitle(child, currentLevel + 1, targetLevel);
            }
        }
    }

    private void CollectAllDescendants(OrganogramTreeNodeDto node, List<OrganogramTreeNodeDto> allDescendants)
    {
        foreach (var child in node.Children)
        {
            var childExcludingChildren = new OrganogramTreeNodeDto
            {
                Id = child.Id,
                Title = child.Title,
                SubTitle = child.SubTitle,
                EntityId = child.EntityId,
                EntityParentId = child.EntityParentId,
                LabelValues = child.LabelValues,
                Config = child.Config,
                Children = new List<OrganogramTreeNodeDto>() //remove children when adding to the flat list
            };
            allDescendants.Add(childExcludingChildren);
            CollectAllDescendants(child, allDescendants);
        }
    }

    private OrganogramTreeNodeDto BuildEmployeeNode(Employee employee)
    {
        var hasDifferentManager = employee.ManagerId != null && employee.ManagerId != employee.EmployeeId; //if the employee has a manager and the manager is not the employee itself

        return new OrganogramTreeNodeDto
        {
            Id = "employee" + employee.EmployeeId,
            Title = string.Join(" ", [employee.FirstName, employee.LastName]),
            SubTitle = employee.Code + (employee.Disabled ? " - DISABLED" : ""),
            EntityId = employee.EmployeeId,
            EntityParentId = hasDifferentManager ? employee.ManagerId : null,
            LabelValues = new Dictionary<string, string>
            {
                { "Is Disabled", employee.Disabled.ToString() },
                { "Job Title", employee.EmployeeJobTitles?.Select(s => s?.EmployeeJobTitle?.Name).ToList().StringJoin(", ") },
                { "Department Group", employee.EmployeeDepartments?.Select(d => d?.EngageDepartment.EngageDepartmentGroup.Name).Distinct().ToList().StringJoin(", ") },
                { "Region", employee.EmployeeRegions?.Select(x => x.EngageRegion.Name).ToList().StringJoin(", ") },
            },
            Config = new OrganogramTreeNodeConfig
            {
                SummaryNode = new BaseOrganogramTreeNodeConfig
                {
                    LabelValueConfig = new BaseOrganogramLabelValueConfig
                    {
                        IsLabelsHidden = true,
                        IsLabelValuesHidden = false,
                        ExcludedLabels = new List<string> { "Job Title", "Region", "Is Disabled" }
                    }
                },
                SubTitleConfig = new OrganogramSubTitleConfig
                {
                    Variant = OrganogramTitleVariant.Variant2
                }
            },
            Children = new List<OrganogramTreeNodeDto>()
        };
    }

    //Filter the tree to exclude disabled employees without subordinates
    private List<OrganogramTreeNodeDto> FilterTree(List<OrganogramTreeNodeDto> nodes)
    {
        var filteredNodes = new List<OrganogramTreeNodeDto>();

        foreach (var node in nodes)
        {
            node.Children = FilterTree(node.Children.ToList());

            // Include the node if it is not disabled or it has children
            if (!IsDisabled(node) || node.Children.Any())
            {
                filteredNodes.Add(node);
            }
        }

        return filteredNodes;
    }

    private bool IsDisabled(OrganogramTreeNodeDto node)
    {
        if (node == null || node.LabelValues.IsNullOrEmpty())
        {
            return true;
        }

        return node.LabelValues.TryGetValue("Is Disabled", out var isDisabled) && bool.TryParse(isDisabled, out var result) && result;
    }


}
