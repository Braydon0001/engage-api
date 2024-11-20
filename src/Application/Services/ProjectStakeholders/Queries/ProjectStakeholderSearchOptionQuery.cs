namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderSearchOptionQuery : IRequest<List<ProjectStakeholderSearchOptionDto>>
{
    public int? StoreId { get; set; }
    public int? ProjectSubCategoryId { get; set; }
    public string SupplierIds { get; set; }
    public string BrandIds { get; set; }
    public string Search { get; set; }
}
public record ProjectStakeholderSearchOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStakeholderSearchOptionQuery, List<ProjectStakeholderSearchOptionDto>>
{
    public async Task<List<ProjectStakeholderSearchOptionDto>> Handle(ProjectStakeholderSearchOptionQuery query, CancellationToken cancellationToken)
    {
        List<ProjectStakeholderSearchOptionDto> returnData = [];
        var hasSparBrands = true;

        ProjectStakeholderSearchOptionDto userSearchOptions = null;
        ProjectStakeholderSearchOptionDto storeUserSearchOptions = null;
        ProjectStakeholderSearchOptionDto storeCallcycleSearchOptions = null;

        if (query.SupplierIds.IsNotNullOrEmpty())
        {
            List<int> SupplierIds = query.SupplierIds.Split(',').Select(int.Parse).ToList();

            //Checking if supplier has Spar brand / a selected brand is spar brand
            if (query.BrandIds.IsNotNullOrEmpty())
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
            var supplierUsers = await Context.Users.Where(e => SupplierIds.Contains(e.SupplierId)
                                                          && (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                            || EF.Functions.Like(e.Email, $"%{query.Search}%")))
                                       .Take(40)
                                       .Select(e => new ProjectStakeholderSearchOption { Id = e.UserId, Name = $"{e.FullName} - {e.Email}", Identifier = "user" })
                                       .ToListAsync(cancellationToken);

            var employees = await Context.Employees.Where(e => e.UserId.HasValue
                                    && supplierUsers.Select(e => e.Id).Contains(e.UserId.Value))
                                         .ToListAsync(cancellationToken);

            foreach (var user in supplierUsers)
            {
                var employee = employees.FirstOrDefault(e => e.UserId == user.Id);
                if (employee != null && employee.Files.IsNotNullOrEmpty())
                {
                    user.PhotoUrl = employee.Files.Count > 0 && employee.Files.Where(e => e.Type == "photo").Any() ? employee.Files.FirstOrDefault(e => e.Type == "photo").Url : "";
                }
            }


            if (supplierUsers.Count > 0)
            {
                returnData.Add(new ProjectStakeholderSearchOptionDto
                {
                    GroupName = "User",
                    Options = supplierUsers
                });
            }
        }
        else
        {
            var user = await Context.Users.Where(e => EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                || EF.Functions.Like(e.Email, $"%{query.Search}%"))
                                       .Take(50)
                                       .Select(e => new ProjectStakeholderSearchOption { Id = e.UserId, Name = $"{e.FullName} - {e.Email}", Identifier = "user" })
                                       .ToListAsync(cancellationToken);

            var employees = await Context.Employees.Where(e => e.UserId.HasValue
                                    && user.Select(e => e.Id).Contains(e.UserId.Value))
                                         .ToListAsync(cancellationToken);

            foreach (var currentUser in user)
            {
                var employee = employees.FirstOrDefault(e => e.UserId == currentUser.Id);
                if (employee != null && employee.Files.IsNotNullOrEmpty())
                {
                    currentUser.PhotoUrl = employee.Files.FirstOrDefault(e => e.Type == "photo")?.Url;
                }
            }

            if (user.Count > 0)
            {
                //returnData.Add(new ProjectStakeholderSearchOptionDto
                //{
                //    GroupName = "User",
                //    Options = user
                //});
                userSearchOptions = new ProjectStakeholderSearchOptionDto
                {
                    GroupName = "User",
                    Options = user
                };
            }
        }

        var externalUsers = await Context.ProjectExternalUsers.Where(e => EF.Functions.Like(e.FirstName, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.LastName, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.Email, $"%{query.Search}%")
                                                                        || EF.Functions.Like(e.CellNumber, $"%{query.Search}%"))
                                                              .Take(50)
                                                              .Select(e => new ProjectStakeholderSearchOption { Id = e.ProjectExternalUserId, Name = $"{e.FirstName} {e.LastName} - {e.Email}", Identifier = "externalUser" })
                                                              .ToListAsync(cancellationToken);

        if (externalUsers.Count > 0)
        {
            returnData.Add(new ProjectStakeholderSearchOptionDto
            {
                GroupName = "External User",
                Options = externalUsers
            });
        }

        if (query.StoreId.HasValue)
        {
            // store contacts
            var storeContacts = await Context.StoreContacts.Where(e => e.StoreId == query.StoreId.Value
                                                                && (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                                || EF.Functions.Like(e.EmailAddress1, $"%{query.Search}%")))
                                                           .Take(50)
                                                           .Select(e => new ProjectStakeholderSearchOption
                                                           {
                                                               Id = e.EntityContactId,
                                                               Name = e.FullName,
                                                               Identifier = "storeContact"
                                                           })
                                                           .ToListAsync(cancellationToken);

            // get accelerate users based on call cycles and subGroup

            //var storeUserIds = await Context.EmployeeStores.Where(e => e.Employee.UserId.HasValue && e.StoreId == query.StoreId).Select(e => e.EmployeeId).ToListAsync(cancellationToken);

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
                                                     .Take(50)
                                                     .Select(e => new ProjectStakeholderSearchOption { Id = e.UserId, Name = $"{e.FullName} - {e.Email}", Identifier = "user" })
                                                     .ToListAsync(cancellationToken);

            //ProjectStakeholderSearchOptionDto callCycleUsers = new ProjectStakeholderSearchOptionDto
            //{
            //    GroupName = "Call Cycles",
            //    Options = accelerateUsers
            //};
            storeCallcycleSearchOptions = new ProjectStakeholderSearchOptionDto
            {
                GroupName = "Call Cycles",
                Options = accelerateUsers
            };

            // If Spar brand get Engage users based on call cycles
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

                var userIds = await userCallCycleSearch
                                                          .Where(e => e.Employee.UserId.HasValue && e.StoreId == query.StoreId.Value)
                                                          .Select(e => e.Employee.UserId)
                                                          .ToListAsync(cancellationToken);

                if (userIds.Count > 0)
                {
                    userIds = userIds.Distinct().ToList();
                    var users = await Context.Users.Where(e => userIds.Contains(e.UserId) && (EF.Functions.Like(e.FullName, $"%{query.Search}%")
                                                        || EF.Functions.Like(e.Email, $"%{query.Search}%")))
                                               .Take(50)
                                               .Select(e => new ProjectStakeholderSearchOption { Id = e.UserId, Name = $"{e.FullName} - {e.Email}", Identifier = "user" })
                                               .ToListAsync(cancellationToken);

                    if (users.Count > 0 || accelerateUsers.Count > 0)
                    {
                        users = users.Where(e => !storeCallcycleSearchOptions.Options.Select(f => f.Id).Contains(e.Id)).ToList();
                        storeCallcycleSearchOptions.Options.AddRange(users);
                        //returnData.Add(callCycleUsers);
                    }
                }

                if (storeContacts.Count > 0)
                {
                    //returnData.Add(new ProjectStakeholderSearchOptionDto
                    //{
                    //    GroupName = "Store Contact",
                    //    Options = storeContacts
                    //});
                    storeUserSearchOptions = new ProjectStakeholderSearchOptionDto
                    {
                        GroupName = "Store Contact",
                        Options = storeContacts
                    };
                }
            }

        }

        if (query.SupplierIds.IsNotNullOrWhiteSpace())
        {
            List<int> SupplierIds = query.SupplierIds.Split(',').Select(int.Parse).ToList();

            var subContractorQuery = Context.SubContractorBrands.Where(e =>
                                                                            e.ParentId.HasValue
                                                                            && SupplierIds.Contains(e.ParentId.Value))
                                                                        .AsQueryable();

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

            var subContractContactQueryable = Context.SupplierContacts.Where(e => subContractIds.Contains(e.SupplierId)).Include(e => e.EntityContactRegions).AsQueryable();

            if (query.StoreId.HasValue && currentStore != null)
            {
                subContractContactQueryable = subContractContactQueryable.Where(e => e.EntityContactRegions.Any(f => f.EngageRegionId == currentStore.EngageRegionId)
                                                          || e.EntityContactRegions.Count == 0);
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
                                                                    && (EF.Functions.Like(e.FirstName, $"%{query.Search}%")
                                                                            || EF.Functions.Like(e.LastName, $"%{query.Search}%")
                                                                            || EF.Functions.Like(e.EmailAddress1, $"%{query.Search}%")))
                                                                 .Take(50)
                                                                 .Select(e => new ProjectStakeholderSearchOption
                                                                 {
                                                                     Id = e.EntityContactId,
                                                                     Name = $"{e.FirstName} {e.LastName}",
                                                                     Identifier = "supplierContact"
                                                                 }).ToListAsync(cancellationToken);
            if (supplierContacts.Count > 0 || subContractContacts.Count > 0)
            {
                returnData.Add(new ProjectStakeholderSearchOptionDto
                {
                    GroupName = "Supplier Contact",
                    Options = [.. supplierContacts, .. subContractContacts]
                });
            }

        }
        // users
        if (userSearchOptions != null && userSearchOptions.Options.Count > 0)
        {
            returnData.Add(userSearchOptions);
        }
        // store contacts
        if (storeUserSearchOptions != null && storeUserSearchOptions.Options.Count > 0)
        {
            returnData.Add(storeUserSearchOptions);
        }
        // call cycles
        if (storeCallcycleSearchOptions != null && storeCallcycleSearchOptions.Options.Count > 0)
        {
            if (userSearchOptions != null)
            {
                storeCallcycleSearchOptions.Options = storeCallcycleSearchOptions.Options.Where(e => !userSearchOptions.Options.Select(f => f.Id).ToList().Contains(e.Id)).ToList();
            }
            returnData.Add(storeCallcycleSearchOptions);
        }

        return returnData;
    }
}
