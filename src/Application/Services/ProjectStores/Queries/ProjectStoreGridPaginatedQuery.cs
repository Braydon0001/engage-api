using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStoreGridPaginatedQuery : PaginatedQuery, IRequest<List<ProjectDto>>
{
    public bool MyIncidents { get; set; }
    public bool AssignedToMe { get; set; }
    public bool IsMobile { get; set; }
    public int? StoreId { get; set; }
    public int? ProjectPriorityId { get; set; }
    public int? ProjectTypeId { get; set; }
    public int? ProjectSubTypeId { get; set; }
    public int? ProjectCategoryId { get; set; }
    public int? ProjectSubCategoryId { get; set; }
    public int? UserId { get; set; }
    public string StakeholderSearch { get; set; }
    public string Search { get; set; }
}

public class ProjectStoreGridPaginatedHandler : InsertHandler, IRequestHandler<ProjectStoreGridPaginatedQuery, List<ProjectDto>>
{
    private readonly IUserService _user;
    public ProjectStoreGridPaginatedHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<List<ProjectDto>> Handle(ProjectStoreGridPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProjectPaginationProps.Create();

        var queryable = _context.ProjectStores.AsQueryable().Include(e => e.EngageBrands).ThenInclude(e => e.EngageBrand).AsNoTracking();

        var user = await _context.Users.Where(e => e.Email.ToLower() == _user.UserName.ToLower()).FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Current user not found");

        var isHostSupplier = _user.IsHostSupplier;

        var currentDate = DateTime.Now.AddHours(2).Date.AddDays(-7);

        var myIncidents = query.MyIncidents;
        var assignedToMe = query.AssignedToMe;

        if (query.FilterModel != null && query.FilterModel.ContainsKey("myIncidents"))
        {
            var incidentFilter = query.FilterModel.FirstOrDefault(e => e.Key == "myIncidents");
            myIncidents = incidentFilter.Value.Filter == "true";
        }

        if (query.FilterModel != null && query.FilterModel.ContainsKey("assignedToMe"))
        {
            var assignedToMeFilter = query.FilterModel.FirstOrDefault(e => e.Key == "assignedToMe");
            assignedToMe = assignedToMeFilter.Value.Filter == "true";
        }

        //if (!isHostSupplier && !query.MyIncidents)
        //{
        //    queryable = queryable.Where(e => e.Owner.SupplierId == _user.SupplierId);
        //    var supplierTags = await _context.ProjectSuppliers.Where(e => e.SupplierId == _user.SupplierId
        //                                                             && (e.Project.ProjectStatusId != (int)ProjectStatusId.Completed
        //                                                                || (e.Project.ProjectStatusId == (int)ProjectStatusId.Completed
        //                                                                && e.Project.ClosedDate.Value.Date >= currentDate)))
        //                                                    .Take(query.PageSize)
        //                                                    .Select(e => e.ProjectId)
        //                                                    .ToListAsync(cancellationToken);

        //    var stakeholderFilter = await _context.ProjectStakeholderUsers.Where(e => e.User.SupplierId == _user.SupplierId
        //                                                                        && (e.Project.ProjectStatusId != (int)ProjectStatusId.Completed
        //                                                                        || (e.Project.ProjectStatusId == (int)ProjectStatusId.Completed
        //                                                                        && e.Project.ClosedDate.Value.Date >= currentDate)))
        //                                                                  .Take(query.PageSize)
        //                                                                  .Select(e => e.ProjectId)
        //                                                                  .ToListAsync(cancellationToken);

        //    List<int> supplierProjectIds = [.. supplierTags, .. stakeholderFilter];

        //    queryable = queryable.Where(e => supplierProjectIds.Contains(e.ProjectId));

        //}

        if (myIncidents)
        {

            if (assignedToMe)
            {
                //TODO: Currently fetches all stakeholder entries of the current user. preformance concern
                var userStakeholderIds = await _context.ProjectStakeholderUsers.AsNoTracking()
                                                                            .Where(e => e.UserId == user.UserId)
                                                                            .Select(e => e.ProjectStakeholderId)
                                                                            .ToListAsync(cancellationToken);

                var userAssigned = await _context.ProjectAssignees.Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                                 .Select(e => e.ProjectId)
                                                                 .ToListAsync(cancellationToken);

                queryable = queryable.Where(e => userAssigned.Contains(e.ProjectId));
            }
            else
            {
                queryable = queryable.Where(e => e.OwnerId == user.UserId);
            }
        }

        if (query.IsMobile)
        {
            var stakeHolderProjectIds = await _context.ProjectStakeholderUsers.AsNoTracking()
                                                                           .Where(e => e.UserId == user.UserId)
                                                                           .Select(e => e.ProjectId)
                                                                           .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => stakeHolderProjectIds.Contains(e.ProjectId));
        }

        if (query.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == query.StoreId);
        }

        if (query.ProjectTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectTypeId == query.ProjectTypeId);
        }

        if (query.ProjectSubTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectSubTypeId == query.ProjectSubTypeId);
        }

        if (query.ProjectPriorityId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectPriorityId == query.ProjectPriorityId);
        }

        if (query.ProjectCategoryId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectCategoryId == query.ProjectCategoryId);
        }

        if (query.ProjectSubCategoryId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectSubCategoryId == query.ProjectSubCategoryId);
        }

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ProjectPriorityId).ThenByDescending(e => e.Created);
        }

        if (query.UserId.HasValue)
        {
            queryable = queryable.Where(e => e.OwnerId == query.UserId.Value);
        }

        if (query.Search.IsNotNullOrWhiteSpace())
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.ProjectType.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.ProjectSubType.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.ProjectCategory.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.ProjectSubCategory.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.Store.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.Name, $"%{query.Search}%"))
                                          || (EF.Functions.Like(e.ProjectPriority.Name, $"%{query.Search}%"))
                                        );
        }

        #region Custom Filters
        if (query.FilterModel != null)
        {
            query.FilterModel.TryGetValue("engageBrandNames", out FilterModel engageBrands);
            if (engageBrands != null && engageBrands.Values.Count > 0)
            {
                var engageBrandIds = await _context.ProjectEngageBrands.Where(e => engageBrands.Values.Contains(e.EngageBrandId))
                                                                       .Select(e => e.ProjectId)
                                                                       .ToListAsync(cancellationToken);
                queryable = queryable.Where(e => engageBrandIds.Contains(e.ProjectId));
            }
        }
        #endregion

        if (query.FilterModel.IsNullOrEmpty())
        {
            queryable = queryable.Where(e => e.ProjectStatusId != (int)ProjectStatusId.Completed
                                            || (e.ProjectStatusId == (int)ProjectStatusId.Completed && e.ClosedDate.Value.Date >= currentDate));
        }

        var data = await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        var fetchedIds = data.Select(e => e.Id).ToList();

        var stakeholders = await _context.ProjectAssignees.AsNoTracking()
                                                         .Where(e => fetchedIds.Contains(e.ProjectId))
                                                         .Include(e => e.ProjectStakeholder)
                                                         .Select(e => e.ProjectStakeholder)
                                                         .ToListAsync(cancellationToken);

        var usersAssigned = stakeholders.OfType<ProjectStakeholderUser>().Select(e => e.UserId).ToList();
        var externalUserAssigned = stakeholders.OfType<ProjectStakeholderExternalUser>().Select(e => e.ProjectExternalUserId).ToList();
        var storeContactAssigned = stakeholders.OfType<ProjectStakeholderStoreContact>().Select(e => e.StoreContactId).ToList();
        var supplierContactAssigned = stakeholders.OfType<ProjectStakeholderSupplierContact>().Select(e => e.SupplierContactId).ToList();
        var regionContactAssigned = stakeholders.OfType<ProjectStakeholderEmployeeRegionContact>().Select(e => e.EmployeeRegionContactId).ToList();

        var userOptions = await _context.Users.AsNoTracking()
                                                     .Where(e => usersAssigned.Contains(e.UserId))
                                                     .ToListAsync(cancellationToken);

        var externalUserOptions = await _context.ProjectExternalUsers.AsNoTracking()
                                                                    .Where(e => externalUserAssigned.Contains(e.ProjectExternalUserId))
                                                                    .ToListAsync(cancellationToken);

        var storeContactOptions = await _context.StoreContacts.Where(e => storeContactAssigned.Contains(e.EntityContactId))
                                                                                             .ToListAsync(cancellationToken);

        var supplierContactOptions = await _context.SupplierContacts.Where(e => supplierContactAssigned.Contains(e.EntityContactId))
                                                                                                      .ToListAsync(cancellationToken);

        var regionContactOptions = await _context.EngageRegionContacts.Where(e => regionContactAssigned.Contains(e.EntityContactId))
                                                                                                       .ToListAsync(cancellationToken);

        foreach (var project in data)
        {
            var projectUserIds = stakeholders.OfType<ProjectStakeholderUser>().Where(e => e.ProjectId == project.Id).Select(e => e.UserId);
            var externalUserIds = stakeholders.OfType<ProjectStakeholderExternalUser>().Where(e => e.ProjectId == project.Id).Select(e => e.ProjectExternalUserId);
            var storeContactIds = stakeholders.OfType<ProjectStakeholderStoreContact>().Where(e => e.ProjectId == project.Id).Select(e => e.StoreContactId);
            var supplierContactIds = stakeholders.OfType<ProjectStakeholderSupplierContact>().Where(e => e.ProjectId == project.Id).Select(e => e.SupplierContactId).ToList();
            var regionContactIds = stakeholders.OfType<ProjectStakeholderEmployeeRegionContact>().Where(e => e.ProjectId == project.Id).Select(e => e.EmployeeRegionContactId).ToList();

            var projectUsers = userOptions.Where(e => projectUserIds.Contains(e.UserId)).Select(e => $"{e.FirstName} {e.LastName}").ToList();
            var externalUsers = externalUserOptions.Where(e => externalUserIds.Contains(e.ProjectExternalUserId)).Select(e => $"{e.FirstName} {e.LastName}").ToList();
            var storeContacts = storeContactOptions.Where(e => storeContactIds.Contains(e.EntityContactId)).Select(e => $"{e.FirstName} {e.LastName}").ToList();
            var supplierContacts = supplierContactOptions.Where(e => supplierContactIds.Contains(e.EntityContactId)).Select(e => $"{e.FirstName} {e.LastName}").ToList();
            var regionContacts = regionContactOptions.Where(e => regionContactIds.Contains(e.EntityContactId)).Select(e => $"{e.FirstName} {e.LastName}").ToList();

            project.ProjectAssignedTo = string.Join(", ", [.. projectUsers, .. externalUsers, .. storeContacts, .. supplierContacts, .. regionContacts]);
        }

        return data;
    }
}