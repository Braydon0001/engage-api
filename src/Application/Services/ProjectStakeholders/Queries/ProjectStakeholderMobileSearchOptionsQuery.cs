namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderMobileSearchOptionsQuery : IRequest<List<ProjectStakeholderSearchOption>>
{
    public int? StoreId { get; set; }
    public int? ProjectSubCategoryId { get; set; }
    public string SupplierIds { get; set; }
    public string BrandIds { get; set; }
    public string Search { get; set; }
}

public record ProjectStakeholderMobileSearchOptionsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStakeholderMobileSearchOptionsQuery, List<ProjectStakeholderSearchOption>>
{
    public async Task<List<ProjectStakeholderSearchOption>> Handle(ProjectStakeholderMobileSearchOptionsQuery query, CancellationToken cancellationToken)
    {
        //get indivudal lists of users, storeContacts, ect. to allow each list to be distincted.
        List<ProjectStakeholderSearchOption> usersList = [];
        List<ProjectStakeholderSearchOption> storeContactsList = [];
        List<ProjectStakeholderSearchOption> suppliercontactsList = [];
        List<ProjectStakeholderSearchOption> externalusersList = [];

        var hasSparBrands = true;


        if (query.SupplierIds.IsNotNullOrWhiteSpace())
        {
            List<int> SupplierIds = query.SupplierIds.Split(',').Select(int.Parse).ToList();

            //Checking if supplier has Spar brand / a selected brand is spar brand
            if (query.BrandIds.IsNotNullOrWhiteSpace())
            {
                List<int> brandIds = query.BrandIds.Split(',').Select(int.Parse).ToList();
                var brands = await Context.EngageBrands.Where(e => brandIds.Contains(e.Id)).ToListAsync(cancellationToken);
                hasSparBrands = brands.Any(e => e.IsSparBrand);
            }
            else
            {
                var suppliers = await Context.Suppliers.Where(e => SupplierIds.Contains(e.SupplierId))
                                                       .Include(e => e.SupplierEngageBrands)
                                                       .ThenInclude(e => e.EngageBrand)
                                                       .ToListAsync(cancellationToken);

                if (suppliers.Count > 0)
                {
                    hasSparBrands = suppliers.Any(e => e.SupplierEngageBrands.Any(f => f.EngageBrand.IsSparBrand));
                }
            }

            //getting users based on supplier
            var users = await Context.Users.Where(e => SupplierIds.Contains(e.SupplierId)
                                                     && (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                            || EF.Functions.Like(e.Email, $"%{query.Search}%")))
                                       .Take(20)
                                       .Select(e => new ProjectStakeholderSearchOption
                                       {
                                           Id = e.UserId,
                                           Name = $"{e.FullName} - {e.Email}",
                                           Identifier = "user"
                                       })
                                       .ToListAsync(cancellationToken);

            usersList.AddRange(users);

            //returnData.AddRange(users);
        }
        else
        {
            var users = await Context.Users.Where(e => EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                || EF.Functions.Like(e.Email, $"%{query.Search}%"))
                                       .Take(20)
                                       .Select(e => new ProjectStakeholderSearchOption
                                       {
                                           Id = e.UserId,
                                           Name = $"{e.FullName} - {e.Email}",
                                           Identifier = "user"
                                       })
                                       .ToListAsync(cancellationToken);

            usersList.AddRange(users);

            //returnData.AddRange(users);
        }

        var externalUsers = await Context.ProjectExternalUsers.Where(e => EF.Functions.Like(e.FirstName, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.LastName, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.Email, $"%{query.Search}%"))
                                                              .Take(20)
                                                              .Select(e => new ProjectStakeholderSearchOption
                                                              {
                                                                  Id = e.ProjectExternalUserId,
                                                                  Name = $"{e.FirstName} {e.LastName} - {e.Email}",
                                                                  Identifier = "externalUser"
                                                              })
                                                              .ToListAsync(cancellationToken);

        externalusersList.AddRange(externalUsers);

        if (query.StoreId.HasValue)
        {
            var storeContacts = await Context.StoreContacts.Where(e => e.StoreId == query.StoreId
                                                                && (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.EmailAddress1, $"%{query.Search}%")))
                                                            .Take(20)
                                                            .Select(e => new ProjectStakeholderSearchOption
                                                            {
                                                                Id = e.EntityContactId,
                                                                Name = $"{e.FullName} - {e.EmailAddress1}",
                                                                Identifier = "storeContact"
                                                            }).ToListAsync(cancellationToken);

            //var storeUserIds = await Context.EmployeeStores.Where(e => e.Employee.UserId.HasValue && e.StoreId == query.StoreId).Select(e => e.EmployeeId).ToListAsync(cancellationToken);

            // get accelerate users based on call cycles and subGroup
            var acceleteUserCallCycles = Context.EmployeeStores.Where(e => e.Employee.UserId.HasValue && e.StoreId == query.StoreId).AsQueryable();

            if (query.ProjectSubCategoryId.HasValue)
            {
                var subCategory = await Context.ProjectSubCategories.Include(e => e.EngageSubGroup)
                                                                        .FirstOrDefaultAsync(e => e.ProjectSubCategoryId == query.ProjectSubCategoryId.Value, cancellationToken);

                if (subCategory != null && subCategory.EngageSubGroup != null)
                {
                    acceleteUserCallCycles = acceleteUserCallCycles.Where(e => e.EngageSubGroupId == subCategory.EngageSubGroupId.Value);
                }
            }

            var storeUserIds = await acceleteUserCallCycles.Select(e => e.EmployeeId).ToListAsync(cancellationToken);

            var userDivisionIds = await Context.EmployeeEmployeeDivisions.Where(e => storeUserIds.Distinct().Contains(e.EmployeeId) && e.EmployeeDivision.IsRihCallCycles == true)
                                                                   .Select(e => e.Employee.UserId)
                                                                   .ToListAsync(cancellationToken);

            var accelerateUsers = await Context.Users.Where(e => userDivisionIds.Distinct().Contains(e.UserId))
                                                     .Take(20)
                                                     .Select(e => new ProjectStakeholderSearchOption { Id = e.UserId, Name = $"{e.FullName} - {e.Email}", Identifier = "user" })
                                                     .ToListAsync(cancellationToken);

            //get engage users based on callcycles and subGroup
            if (hasSparBrands)
            {
                var userCallCycleSearch = Context.EmployeeStores.Include(e => e.Employee).AsQueryable();

                if (query.ProjectSubCategoryId.HasValue)
                {
                    var subCategory = await Context.ProjectSubCategories.Include(e => e.EngageSubGroup)
                                                                        .FirstOrDefaultAsync(e => e.ProjectSubCategoryId == query.ProjectSubCategoryId.Value, cancellationToken);

                    if (subCategory != null && subCategory.EngageSubGroup != null)
                    {
                        userCallCycleSearch = userCallCycleSearch.Where(e => e.EngageSubGroupId == subCategory.EngageSubGroupId.Value);
                    }
                }

                var userIds = await userCallCycleSearch.Where(e => e.Employee.UserId.HasValue && e.StoreId == query.StoreId.Value)
                                                       .Select(e => e.Employee.UserId)
                                                       .ToListAsync(cancellationToken);

                if (userIds.Count > 0)
                {
                    userIds = userIds.Distinct().ToList();
                    var users = await Context.Users.Where(e => userIds.Contains(e.UserId) && (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                        || EF.Functions.Like(e.Email, $"%{query.Search}%")))
                                               .Take(20)
                                               .Select(e => new ProjectStakeholderSearchOption { Id = e.UserId, Name = $"{e.FullName} - {e.Email}", Identifier = "user" })
                                               .ToListAsync(cancellationToken);

                    if (users.Count > 0 || accelerateUsers.Count > 0)
                    {
                        usersList.AddRange([.. users, .. accelerateUsers]);
                        //returnData.AddRange([.. users, .. accelerateUsers]);
                    }
                }
            }
            else if (accelerateUsers.Count > 0)
            {
                usersList.AddRange(accelerateUsers);
                //returnData.AddRange(accelerateUsers);
            }

            storeContactsList.AddRange(storeContacts);
            //returnData.AddRange(storeContacts);
        }

        if (query.SupplierIds.IsNotNullOrEmpty())
        {
            List<int> SupplierIds = query.SupplierIds.Split(',').Select(int.Parse).ToList();

            var subContractorQuery = Context.SubContractorBrands.Where(e =>
                                                                            e.ParentId.HasValue
                                                                            && SupplierIds.Contains(e.ParentId.Value))
                                                                        .AsQueryable();
            // get subcontractors by region / brand if available
            Store currentStore = null;

            if (query.StoreId.HasValue)
            {
                currentStore = await Context.Stores.Where(e => e.StoreId == query.StoreId.Value).FirstOrDefaultAsync(cancellationToken);
                if (currentStore != null)
                {
                    subContractorQuery = subContractorQuery.Where(e => e.EngageRegionId == currentStore.EngageRegionId);
                }
            }

            if (query.BrandIds.IsNotNullOrWhiteSpace())
            {
                List<int> brandIds = query.BrandIds.Split(',').Select(int.Parse).ToList();
                subContractorQuery = subContractorQuery.Where(e => brandIds.Contains(e.EngageBrandId));
            }

            var subContractIds = await subContractorQuery.Select(e => e.SupplierId).ToListAsync(cancellationToken);

            var subContractContactQueryable = Context.SupplierContacts.Where(e => subContractIds.Contains(e.SupplierId));

            if (query.StoreId.HasValue && currentStore != null)
            {
                subContractContactQueryable = subContractContactQueryable.Where(e => e.EntityContactRegions.Any(f => f.EngageRegionId == currentStore.EngageRegionId) || e.EntityContactRegions.Count == 0);
            }

            var subContractContacts = await subContractContactQueryable.Where(e => (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                                            || EF.Functions.Like(e.EmailAddress1, $"%{query.Search}%")))
                                                                 .Take(20)
                                                                 .Select(e => new ProjectStakeholderSearchOption
                                                                 {
                                                                     Id = e.EntityContactId,
                                                                     Name = $"{e.FullName} - {e.EmailAddress1}",
                                                                     Identifier = "supplierContact"
                                                                 }).ToListAsync(cancellationToken);

            var supplierContacts = await Context.SupplierContacts.Where(e => SupplierIds.Contains(e.SupplierId)
                                                                    && (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                                            || EF.Functions.Like(e.EmailAddress1, $"%{query.Search}%")))
                                                                 .Take(20)
                                                                 .Select(e => new ProjectStakeholderSearchOption
                                                                 {
                                                                     Id = e.EntityContactId,
                                                                     Name = $"{e.FullName} - {e.EmailAddress1}",
                                                                     Identifier = "supplierContact"
                                                                 }).ToListAsync(cancellationToken);

            suppliercontactsList.AddRange([.. subContractContacts, .. supplierContacts]);
            //returnData.AddRange([.. subContractContacts, .. supplierContacts]);
        }

        usersList = usersList.DistinctBy(e => e.Id).ToList();

        return [.. usersList, .. storeContactsList, .. suppliercontactsList, .. externalusersList];
    }
}