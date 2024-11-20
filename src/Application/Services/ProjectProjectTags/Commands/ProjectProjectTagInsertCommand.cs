namespace Engage.Application.Services.ProjectProjectTags.Commands;

public class ProjectProjectTagInsertCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<int> ClaimIds { get; set; }
    public List<int> DCProductIds { get; set; }
    public List<int> EmployeeJobTitleIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public List<int> StoreIds { get; set; }
    public List<int> StoreAssetIds { get; set; }
    public List<int> SupplierIds { get; set; }
    public List<int> UserIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectProjectTagInsertCommand, ProjectProjectTag>();
    }
}

public record ProjectProjectTagInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectProjectTagInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectProjectTagInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.ProjectId < 1)
        {
            throw new Exception("Project not found");
        }

        var stakeholderUserIds = new List<int>();
        var stakeholderEmployeeRegionContactIds = new List<int>();
        var stakeholderStoreContactIds = new List<int>();
        var stakeholderSupplierContactIds = new List<int>();

        if (command.ClaimIds != null)
        {
            if (command.ClaimIds.Count > 0)
            {
                var existingClaimIds = await Context.ProjectProjectTagClaims.Where(e => e.ProjectId == command.ProjectId)
                                                                            .Select(e => e.ClaimId)
                                                                            .ToListAsync();

                var claimIds = command.ClaimIds.Except(existingClaimIds).ToList();
                if (claimIds.Count > 0)
                {
                    foreach (var claimId in claimIds)
                    {
                        var projectProjectTagClaim = new ProjectProjectTagClaim
                        {
                            ProjectId = command.ProjectId,
                            ClaimId = claimId
                        };

                        Context.ProjectProjectTagClaims.Add(projectProjectTagClaim);
                    }
                }
            }
        }

        if (command.DCProductIds != null)
        {
            if (command.DCProductIds.Count > 0)
            {
                var existingDCProductIds = await Context.ProjectProjectTagDCProducts.Where(e => e.ProjectId == command.ProjectId)
                                                                                    .Select(e => e.DCProductId)
                                                                                    .ToListAsync();

                var dcProductIds = command.DCProductIds.Except(existingDCProductIds).ToList();
                if (dcProductIds.Count > 0)
                {
                    foreach (var dcProductId in dcProductIds)
                    {
                        var projectProjectTagDCProduct = new ProjectProjectTagDCProduct
                        {
                            ProjectId = command.ProjectId,
                            DCProductId = dcProductId
                        };

                        Context.ProjectProjectTagDCProducts.Add(projectProjectTagDCProduct);
                    }
                }
            }
        }

        if (command.EmployeeJobTitleIds != null)
        {
            if (command.EmployeeJobTitleIds.Count > 0)
            {
                var existingEmployeeJobTitleIds = await Context.ProjectProjectTagEmployeeJobTitles.Where(e => e.ProjectId == command.ProjectId)
                                                                                                  .Select(e => e.EmployeeJobTitleId)
                                                                                                  .ToListAsync();

                var employeeJobTitleIds = command.EmployeeJobTitleIds.Except(existingEmployeeJobTitleIds).ToList();
                if (employeeJobTitleIds.Count > 0)
                {
                    foreach (var employeeJobTitleId in employeeJobTitleIds)
                    {
                        var projectProjectTagEmployeeJobTitle = new ProjectProjectTagEmployeeJobTitle
                        {
                            ProjectId = command.ProjectId,
                            EmployeeJobTitleId = employeeJobTitleId
                        };

                        Context.ProjectProjectTagEmployeeJobTitles.Add(projectProjectTagEmployeeJobTitle);
                    }
                }

                var userIds = await Context.EmployeeEmployeeJobTitles.Where(e => employeeJobTitleIds.Contains(e.EmployeeJobTitleId))
                                                                     .Select(e => e.Employee.UserId)
                                                                     .ToListAsync();

                if (userIds != null && userIds.Count > 0)
                {
                    userIds = userIds.Distinct().ToList();
                    foreach (var userId in userIds)
                    {
                        if (userId != null)
                        {
                            stakeholderUserIds.Add(userId.Value);
                        }
                    }
                }
            }
        }

        if (command.EngageRegionIds != null)
        {
            if (command.EngageRegionIds.Count > 0)
            {
                var existingEngageRegionIds = await Context.ProjectProjectTagEngageRegions.Where(e => e.ProjectId == command.ProjectId)
                                                                                          .Select(e => e.EngageRegionId)
                                                                                          .ToListAsync();

                var engageRegionIds = command.EngageRegionIds.Except(existingEngageRegionIds).ToList();
                if (engageRegionIds.Count > 0)
                {
                    foreach (var engageRegionId in engageRegionIds)
                    {
                        var projectProjectTagEngageRegion = new ProjectProjectTagEngageRegion
                        {
                            ProjectId = command.ProjectId,
                            EngageRegionId = engageRegionId
                        };

                        Context.ProjectProjectTagEngageRegions.Add(projectProjectTagEngageRegion);
                    }
                }

                var regionContactIds = await Context.EmployeeRegionContacts.Where(e => engageRegionIds.Contains(e.EngageRegionId))
                                                                           .Select(e => e.EmployeeRegionContactId)
                                                                           .ToListAsync();

                if (regionContactIds != null && regionContactIds.Count > 0)
                {
                    foreach (var regionContactId in regionContactIds)
                    {
                        stakeholderEmployeeRegionContactIds.Add(regionContactId);
                    }
                }
            }
        }

        if (command.StoreIds != null)
        {
            if (command.StoreIds.Count > 0)
            {
                var existingStoreIds = await Context.ProjectProjectTagStores.Where(e => e.ProjectId == command.ProjectId)
                                                                            .Select(e => e.StoreId)
                                                                            .ToListAsync();

                var storeIds = command.StoreIds.Except(existingStoreIds).ToList();
                if (storeIds.Count > 0)
                {
                    foreach (var storeId in storeIds)
                    {
                        var projectProjectTagStore = new ProjectProjectTagStore
                        {
                            ProjectId = command.ProjectId,
                            StoreId = storeId
                        };

                        Context.ProjectProjectTagStores.Add(projectProjectTagStore);
                    }
                }

                var storeContactIds = await Context.StoreContacts.Where(e => storeIds.Contains(e.StoreId))
                                                                 .Select(e => e.EntityContactId)
                                                                 .ToListAsync();

                if (storeContactIds != null && storeContactIds.Count > 0)
                {
                    stakeholderStoreContactIds.AddRange(storeContactIds);
                }

                var employeeUserIds = await Context.EmployeeStores.Where(e => storeIds.Contains(e.StoreId))
                                                                  .Select(e => e.Employee.UserId)
                                                                  .ToListAsync();

                if (employeeUserIds != null && employeeUserIds.Count > 0)
                {
                    employeeUserIds = employeeUserIds.Distinct().ToList();
                    foreach (var userId in employeeUserIds)
                    {
                        if (userId != null)
                        {
                            stakeholderUserIds.Add(userId.Value);
                        }
                    }
                }
            }
        }

        if (command.StoreAssetIds != null)
        {
            if (command.StoreAssetIds.Count > 0)
            {
                var existingStoreAssetIds = await Context.ProjectProjectTagStoreAssets.Where(e => e.ProjectId == command.ProjectId)
                                                                                      .Select(e => e.StoreAssetId)
                                                                                      .ToListAsync();

                var storeAssetIds = command.StoreAssetIds.Except(existingStoreAssetIds).ToList();
                if (storeAssetIds.Count > 0)
                {
                    foreach (var storeAssetId in storeAssetIds)
                    {
                        var projectProjectTagStoreAsset = new ProjectProjectTagStoreAsset
                        {
                            ProjectId = command.ProjectId,
                            StoreAssetId = storeAssetId
                        };

                        Context.ProjectProjectTagStoreAssets.Add(projectProjectTagStoreAsset);
                    }
                }
            }
        }

        if (command.SupplierIds != null)
        {
            if (command.SupplierIds.Count > 0)
            {
                var existingSupplierIds = await Context.ProjectProjectTagSuppliers.Where(e => e.ProjectId == command.ProjectId)
                                                                                  .Select(e => e.SupplierId)
                                                                                  .ToListAsync();

                var supplierIds = command.SupplierIds.Except(existingSupplierIds).ToList();
                if (supplierIds.Count > 0)
                {
                    foreach (var supplierId in supplierIds)
                    {
                        var projectProjectTagSupplier = new ProjectProjectTagSupplier
                        {
                            ProjectId = command.ProjectId,
                            SupplierId = supplierId
                        };

                        Context.ProjectProjectTagSuppliers.Add(projectProjectTagSupplier);
                    }
                }

                var supplierContactIds = await Context.SupplierContacts.Where(e => supplierIds.Contains(e.SupplierId))
                                                                       .Select(e => e.EntityContactId)
                                                                       .ToListAsync();

                if (supplierContactIds != null && supplierContactIds.Count > 0)
                {
                    supplierContactIds = supplierContactIds.Distinct().ToList();
                    stakeholderSupplierContactIds.AddRange(supplierContactIds);
                }

                var storeProject = await Context.ProjectStores.Where(e => e.ProjectId == command.ProjectId)
                                                              .FirstOrDefaultAsync(cancellationToken);
                if (storeProject != null)
                {
                    var supplierUserIds = await Context.UserStores.Where(e => e.StoreId == storeProject.StoreId && supplierIds.Contains(e.User.SupplierId))
                                                                  .Select(e => e.UserId)
                                                                  .ToListAsync();

                    if (supplierUserIds != null && supplierUserIds.Count > 0)
                    {
                        stakeholderUserIds.AddRange(supplierUserIds);
                    }
                }
            }
        }

        if (command.UserIds != null)
        {
            if (command.UserIds.Count > 0)
            {
                var existingUserIds = await Context.ProjectProjectTagUsers.Where(e => e.ProjectId == command.ProjectId)
                                                                          .Select(e => e.UserId)
                                                                          .ToListAsync();

                var userIds = command.UserIds.Except(existingUserIds).ToList();
                if (userIds.Count > 0)
                {
                    foreach (var userId in userIds)
                    {
                        var projectProjectTagUser = new ProjectProjectTagUser
                        {
                            ProjectId = command.ProjectId,
                            UserId = userId
                        };

                        Context.ProjectProjectTagUsers.Add(projectProjectTagUser);
                    }
                }
            }
        }

        //Stakeholders
        if (stakeholderUserIds != null)
        {
            if (stakeholderUserIds.Count > 0)
            {
                stakeholderUserIds = stakeholderUserIds.Distinct().ToList();

                var existingUserIds = await Context.ProjectStakeholderUsers.Where(e => e.ProjectId == command.ProjectId)
                                                                           .Select(e => e.UserId)
                                                                           .ToListAsync(cancellationToken);

                var userIds = stakeholderUserIds.Except(existingUserIds).ToList();

                foreach (var userId in userIds.Distinct())
                {
                    var projectStakeholderUser = new ProjectStakeholderUser
                    {
                        ProjectId = command.ProjectId,
                        UserId = userId
                    };

                    Context.ProjectStakeholderUsers.Add(projectStakeholderUser);
                }
            }
        }

        if (stakeholderEmployeeRegionContactIds != null)
        {
            if (stakeholderEmployeeRegionContactIds.Count > 0)
            {
                var existingEmployeeRegionContactIds = await Context.ProjectStakeholderEmployeeRegionContacts.Where(e => e.ProjectId == command.ProjectId)
                                                                                                             .Select(e => e.EmployeeRegionContactId)
                                                                                                             .ToListAsync();

                var employeeRegionContactIds = stakeholderEmployeeRegionContactIds.Except(existingEmployeeRegionContactIds).ToList();

                foreach (var employeeRegionContactId in employeeRegionContactIds)
                {
                    var projectStakeholderEmployeeRegionContact = new ProjectStakeholderEmployeeRegionContact
                    {
                        ProjectId = command.ProjectId,
                        EmployeeRegionContactId = employeeRegionContactId
                    };

                    Context.ProjectStakeholderEmployeeRegionContacts.Add(projectStakeholderEmployeeRegionContact);
                }
            }
        }

        if (stakeholderStoreContactIds != null)
        {
            if (stakeholderStoreContactIds.Count > 0)
            {
                var existingStoreContactIds = await Context.ProjectStakeholderStoreContacts.Where(e => e.ProjectId == command.ProjectId)
                                                                                           .Select(e => e.StoreContactId)
                                                                                           .ToListAsync();

                var storeContactIds = stakeholderStoreContactIds.Except(existingStoreContactIds).ToList();

                foreach (var storeContactId in storeContactIds)
                {
                    var projectStakeholderStoreContact = new ProjectStakeholderStoreContact
                    {
                        ProjectId = command.ProjectId,
                        StoreContactId = storeContactId
                    };

                    Context.ProjectStakeholderStoreContacts.Add(projectStakeholderStoreContact);
                }
            }
        }

        if (stakeholderSupplierContactIds != null)
        {
            if (stakeholderSupplierContactIds.Count > 0)
            {
                var existingSupplierContactIds = await Context.ProjectStakeholderSupplierContacts.Where(e => e.ProjectId == command.ProjectId)
                                                                                                 .Select(e => e.SupplierContactId)
                                                                                                 .ToListAsync();

                var supplierContactIds = stakeholderSupplierContactIds.Except(existingSupplierContactIds).ToList();

                foreach (var supplierContactId in supplierContactIds)
                {
                    var projectStakeholderSupplierContact = new ProjectStakeholderSupplierContact
                    {
                        ProjectId = command.ProjectId,
                        SupplierContactId = supplierContactId
                    };

                    Context.ProjectStakeholderSupplierContacts.Add(projectStakeholderSupplierContact);
                }
            }
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class ProjectProjectTagInsertValidator : AbstractValidator<ProjectProjectTagInsertCommand>
{
    public ProjectProjectTagInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
    }
}