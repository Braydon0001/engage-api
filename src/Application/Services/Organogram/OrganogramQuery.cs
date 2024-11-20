using Engage.Domain.Entities;

namespace Engage.Application.Services.Organogram;

public class OrganogramQuery : IRequest<OrganogramTreeNodeDto>
{
    public int DivisionId { get; set; }
    public int? EmployeeId { get; set; }
}
public record OrganogramQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganogramQuery, OrganogramTreeNodeDto>
{
    public async Task<OrganogramTreeNodeDto> Handle(OrganogramQuery query, CancellationToken cancellationToken)
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
                var hierarchicalManagerNode = query.EmployeeId != null ? employeeNodes[query.EmployeeId ?? 247] : null;
                if (hierarchicalManagerNode != null)
                {
                    //any without a manager id including the hierarchy top node
                    var hierarchicalManagerBranch = BuildParentHierarchy(employeeNodes, hierarchicalManagerNode);

                        return new OrganogramTreeNodeDto
                        {
                            Id = "division" + division.EmployeeDivisionId,
                            EntityId = division.EmployeeDivisionId,
                            Title = division.Name,
                            Children = [hierarchicalManagerBranch]
                        };
                }
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


        return new OrganogramTreeNodeDto
        {
            Id = "division" + division.EmployeeDivisionId,
            EntityId = division.EmployeeDivisionId,
            Title = division.Name,
            Children = mainEmployeeNodes
        };
    }


    private OrganogramTreeNodeDto BuildParentHierarchy(Dictionary<int, OrganogramTreeNodeDto> allNodes, OrganogramTreeNodeDto startNode)
    {
        OrganogramTreeNodeDto parentNode = new();
        var currentNode = startNode;

        while (currentNode != null && currentNode.EntityParentId.HasValue)
        {
            var newParentNode = allNodes[currentNode.EntityParentId.Value];
            if (newParentNode != null && newParentNode.EntityId != currentNode.EntityId)
            {
                parentNode = newParentNode;
                parentNode.Children = [currentNode];
                currentNode = parentNode; // Move up the hierarchy
            }
            else
            {
                currentNode = null; // No parent found, stop the loop
            }
        }

        if(parentNode.EntityId != null)
        { return parentNode; }

        return startNode;
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
                { "Department", employee.EmployeeDepartments?.Select(d => d?.EngageDepartment.Name).ToList().StringJoin(", ") },
                { "Region", employee.EmployeeRegions?.Select(x => x.EngageRegion.Name).ToList().StringJoin(", ") },
            },
            Config = new OrganogramTreeNodeConfig
            {
                SubTitleConfig = new OrganogramSubTitleConfig
                {
                    Variant = OrganogramTitleVariant.Variant2
                }
                ,StandardNode = new BaseOrganogramTreeNodeConfig
                {
                    LabelValueConfig = new BaseOrganogramLabelValueConfig
                    {
                        ExcludedLabels = new List<string> { "Is Disabled" },
                    },
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
