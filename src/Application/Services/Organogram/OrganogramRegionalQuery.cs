namespace Engage.Application.Services.Organogram;

public class OrganogramRegionalQuery : IRequest<OrganogramTreeNodeDto>
{
    public int DivisionId { get; set; }
    public int? RegionId { get; set; }
    public int? SubRegionId { get; set; }
    public int? EmployeeId { get; set; }
}
public record OrganogramRegionalQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganogramRegionalQuery, OrganogramTreeNodeDto>
{
    public async Task<OrganogramTreeNodeDto> Handle(OrganogramRegionalQuery query, CancellationToken cancellationToken)
    {
        var division = await Context.EmployeeDivisions.FindAsync(query.DivisionId);

        //var managingDirectorJobTitleId = 77;
        var regionalManagerJobTitleId = 78;
        var nationalManagerJobTitleId = 173;
        List<int> managerJobTitleIds = new([ regionalManagerJobTitleId, nationalManagerJobTitleId ]); //list of Regional/National Manager Job Title Ids i.e the ones that report diectly to the managing director(Shaun Gatonby). All managers under these are considered subregion managers and will show when filtered by subregion

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
            engageRegionQuery = engageRegionQuery.Where(r => r.Id == query.RegionId
                                                          && r.Disabled == false
                                                          && r.Deleted == false);
        }

        var regions = await engageRegionQuery.Where(r =>  r.Disabled == false
                                                      && r.Deleted == false)
                                             .OrderBy(x => x.Name)
                                             .ToListAsync(cancellationToken);

        var regionNodes = new List<OrganogramTreeNodeDto>();

        foreach (var region in regions)
        {
            var engageRegionEmployees = await Context.Employees.IgnoreQueryFilters() //ignore the global query filter for disabled employees
                                                               .Include(f => f.EmployeeJobTitles)
                                                                   .ThenInclude(l => l.EmployeeJobTitle)
                                                               .Include(y => y.EmployeeRegions)
                                                                   .ThenInclude(x => x.EngageRegion)
                                                               .Include(q => q.EngageSubRegion)
                                                               .Include(p => p.EmployeeDivisions)
                                                               .Include(f => f.EmployeeDepartments)
                                                                    .ThenInclude(g => g.EngageDepartment)
                                                               .Where(e => e.EmployeeDivisions
                                                                             .Any(x => x.EmployeeDivisionId == query.DivisionId)
                                                                           && e.EmployeeRegions
                                                                             .Any(y => y.EngageRegion.Id == region.Id)
                                                                           && ((!query.SubRegionId.HasValue || (e.EngageSubRegionId.HasValue && e.EngageSubRegionId == query.SubRegionId)) // if the employee is assigned to the subregion
                                                                           || e.EmployeeJobTitles.Any(j => managerJobTitleIds.Contains(j.EmployeeJobTitleId))) // or if the employee has a regional manager or national manager job title
                                                                        // && e.Disabled == false
                                                                           && e.Deleted == false)
                                                               .AsNoTracking()
                                                               .ToListAsync(cancellationToken);
            List<Employee> engageEmployees = new();

            if (query.EmployeeId.HasValue)
            {
                var engageEmployee = engageRegionEmployees.SingleOrDefault(e => e.EmployeeId == query.EmployeeId);

                if (engageEmployee != null)
                {
                    engageEmployees.Add(engageEmployee);

                    var managerIds = new List<int>();
                    var hasDifferentManager = engageEmployee.ManagerId != null && engageEmployee.ManagerId != engageEmployee.EmployeeId; //if the employee has a manager and the manager is not the employee itself
                    var managerId = hasDifferentManager ? engageEmployee.ManagerId : null;

                    while (managerId != null)
                    {
                        managerIds.Add(managerId.Value);
                        var manager = engageRegionEmployees.SingleOrDefault(e => e.EmployeeId != e.ManagerId && e.EmployeeId == managerId.Value);
                        var regionIdExists = manager?.EmployeeRegions?.Any(r => r.EngageRegion?.Id == region.Id) ?? false;
                        if (regionIdExists)
                        {
                            var managerHasDifferentManager = manager.ManagerId != null && manager.ManagerId != manager.EmployeeId;
                            managerId = managerHasDifferentManager ? manager?.ManagerId : null;
                        }
                        else { break; }
                    }

                    var managers = engageRegionEmployees.Where(e => managerIds.Contains(e.EmployeeId));

                    engageEmployees.AddRange(managers);
                }
            }
            else
            {
                engageEmployees = engageRegionEmployees.ToList();
            }

            var employeeNodes = engageEmployees.ToDictionary(e => e.EmployeeId, e => BuildEmployeeNode(e, "region" + region.Id + "employee" + e.EmployeeId));

            foreach (var employeeNode in employeeNodes.Values)
            {
                var employeeId = employeeNode.EntityId;
                var employee = engageRegionEmployees.FirstOrDefault(e => e.EmployeeId == employeeId);
                var hasDifferentManager = employee.ManagerId != null && employee.ManagerId != employee.EmployeeId; //if the employee has a manager and the manager is not the employee itself
                if (hasDifferentManager)
                {
                    if (employee.ManagerId != null && employeeNodes.ContainsKey(employee.ManagerId.Value))
                    {
                        employeeNodes[employee.ManagerId.Value].Children.Add(employeeNode);
                        employeeNodes[employee.ManagerId.Value].Children = employeeNodes[employee.ManagerId.Value].Children.OrderBy(x => x.Title).ToList();
                    }
                }
            }

            var rootNodes = employeeNodes.Values
                            .Where(n => n.EntityParentId == null
                                        || (query.SubRegionId.HasValue
                                            && (employeeNodes.ContainsKey(n.EntityParentId.Value)
                                                && HasManagerWithSpecificJobTitles(employeeNodes[n.EntityParentId.Value]?.LabelValues["Job Title Ids"], managerJobTitleIds))))
                            .ToList();  //any without a manager id including the hierarchy top node or the ones with a regional manager  as their line manager if filtered by subregion

            if (query.SubRegionId.HasValue) // grouping the subregion managers under their regional manager
            {
                var rootNodeManagerIds = rootNodes.Select(n => n.EntityParentId).Distinct().ToList();
                var rootManagerNodes = employeeNodes.Values.Where(employeeNode => rootNodeManagerIds.Contains(employeeNode.EntityId))
                                                           .ToDictionary(g => g.EntityId, g =>
                                                           {
                                                               g.Children = new List<OrganogramTreeNodeDto>();
                                                               return g;
                                                           });

                foreach (var rootNode in rootNodes)
                {
                    if(rootManagerNodes.ContainsKey(rootNode.EntityParentId.Value))
                    {
                        rootManagerNodes[rootNode.EntityParentId.Value].Children.Add(rootNode);
                        rootManagerNodes[rootNode.EntityParentId.Value].Children.OrderBy(x => x.Title).ToList();
                    }
                }

                rootNodes = rootManagerNodes.Values.ToList();
            }

            // Filter the tree to exclude disabled employees without subordinates
            rootNodes = FilterTree(rootNodes);

            List<OrganogramTreeNodeDto> regionNodeChildren = rootNodes;

            if (query.SubRegionId.HasValue)
            {

                var subRegionNode = new OrganogramTreeNodeDto
                {
                    Id = "subRegion" + query.SubRegionId,
                    Title = subRegion.Name,
                    Children = rootNodes
                };

                regionNodeChildren = [subRegionNode];
            }

            regionNodes.Add(new OrganogramTreeNodeDto
            {
                Id = "region" + region.Id,
                Title = region.Name,
                Children = regionNodeChildren
            });
        }

        return new OrganogramTreeNodeDto
        {
            Id = "division" + division.EmployeeDivisionId,
            EntityId = division.EmployeeDivisionId,
            Title = division.Name,
            Children = regionNodes
        };
    }

    private OrganogramTreeNodeDto BuildEmployeeNode(Employee employee, string nodeId)
    {
        var hasDifferentManager = employee.ManagerId != null && employee.ManagerId != employee.EmployeeId; //if the employee has a manager and the manager is not the employee itself

        return new OrganogramTreeNodeDto
        {
            Id = nodeId,
            Title = string.Join(" ", [employee.FirstName, employee.LastName]),
            SubTitle = employee.Code + (employee.Disabled ? " - DISABLED" : ""),
            EntityId = employee.EmployeeId,
            EntityParentId = hasDifferentManager ? employee.ManagerId : null,
            LabelValues = new Dictionary<string, string>
            {
                { "Is Disabled", employee.Disabled.ToString() },
                { "Job Title Ids", employee.EmployeeJobTitles?.Select(s => s?.EmployeeJobTitle?.EmployeeJobTitleId).ToList().StringJoin("|") },
                { "Job Title", employee.EmployeeJobTitles?.Select(s => s?.EmployeeJobTitle?.Name).ToList().StringJoin(", ") },
                { "Department", employee.EmployeeDepartments?.Select(d => d?.EngageDepartment.Name).ToList().StringJoin(", ") },
                { "Sub Region Id", employee.EngageSubRegionId.ToString()},
                { "Sub Region", employee.EngageSubRegion?.Name ?? "Unnasigned"}
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
                        ExcludedLabels = new List<string> { "Is Disabled", "Sub Region Id", "Job Title Ids" },
                        //ExcludedLabels = new List<string> { "First Name" }
                    }
                },
                SubTitleConfig = new OrganogramSubTitleConfig
                {
                    Variant = OrganogramTitleVariant.Variant2
                },
                StandardNode = new BaseOrganogramTreeNodeConfig
                {
                    LabelValueConfig = new BaseOrganogramLabelValueConfig
                    {
                        ExcludedLabels = new List<string> { "Is Disabled", "Sub Region Id", "Job Title Ids" },
                    },
                },
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

    bool HasManagerWithSpecificJobTitles(string jobTitleIds, List<int> managerJobTitleIds)
    {
        if (string.IsNullOrEmpty(jobTitleIds))
        {
            return false;
        }

        var jobTitleIdArray = jobTitleIds.Split('|').Select(int.Parse).ToArray();
        return jobTitleIdArray.Any(id => managerJobTitleIds.Contains(id));
    }

}
