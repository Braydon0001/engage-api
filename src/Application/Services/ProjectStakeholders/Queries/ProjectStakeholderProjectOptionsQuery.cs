namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderProjectOptionsQuery : IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class ProjectStakeholderProjectOptionsQueryHandler : IRequestHandler<ProjectStakeholderProjectOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ProjectStakeholderProjectOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ProjectStakeholderProjectOptionsQuery request, CancellationToken cancellationToken)
    {
        if (request.ProjectId != 0)
        {
            //var regionContactsQuaryable = _context.ProjectStakeholderEmployeeRegionContacts.Where(e => e.ProjectId == request.ProjectId)
            //                                                                               .AsQueryable();

            //var storeContactsQuaryable = _context.ProjectStakeholderStoreContacts.Where(e => e.ProjectId == request.ProjectId)
            //                                                                     .AsQueryable();

            //var supplierContactsQuaryable = _context.ProjectStakeholderSupplierContacts.Where(e => e.ProjectId == request.ProjectId)
            //                                                                           .AsQueryable();

            var usersQuaryable = _context.ProjectStakeholderUsers.Where(e => e.ProjectId == request.ProjectId)
                                                                 .AsQueryable();

            //if (!string.IsNullOrWhiteSpace(request.Search))
            //{
            //    regionContactsQuaryable = regionContactsQuaryable.Where(e => EF.Functions.Like(e.EmployeeRegionContact.Employee.FirstName, $"%{request.Search}%")
            //                                        || EF.Functions.Like(e.EmployeeRegionContact.Employee.LastName, $"%{request.Search}%")
            //                                        || EF.Functions.Like(e.EmployeeRegionContact.Employee.EmailAddress1, $"%{request.Search}%")
            //                                      );
            //}

            //if (!string.IsNullOrWhiteSpace(request.Search))
            //{
            //    storeContactsQuaryable = storeContactsQuaryable.Where(e => EF.Functions.Like(e.StoreContact.FirstName, $"%{request.Search}%")
            //                                        || EF.Functions.Like(e.StoreContact.LastName, $"%{request.Search}%")
            //                                        || EF.Functions.Like(e.StoreContact.EmailAddress1, $"%{request.Search}%")
            //                                      );
            //}

            //if (!string.IsNullOrWhiteSpace(request.Search))
            //{
            //    supplierContactsQuaryable = supplierContactsQuaryable.Where(e => EF.Functions.Like(e.SupplierContact.FirstName, $"%{request.Search}%")
            //                                        || EF.Functions.Like(e.SupplierContact.LastName, $"%{request.Search}%")
            //                                        || EF.Functions.Like(e.SupplierContact.EmailAddress1, $"%{request.Search}%")
            //                                      );
            //}

            //var regionContacts = await regionContactsQuaryable.Select(e => new OptionDto { Id = e.ProjectStakeholderId, Name = e.EmployeeRegionContact.Employee.FirstName + " " + e.EmployeeRegionContact.Employee.LastName + " - " + e.EmployeeRegionContact.Employee.EmailAddress1 + "(Region Contact)" })
            //                                                  .OrderBy(e => e.Name)
            //                                                  .ToListAsync(cancellationToken);

            //var storeContacts = await storeContactsQuaryable.Select(e => new OptionDto { Id = e.ProjectStakeholderId, Name = e.StoreContact.FirstName + " " + e.StoreContact.LastName + " - " + e.StoreContact.EmailAddress1 + "(Store Contact)" })
            //                                                .OrderBy(e => e.Name)
            //                                                .ToListAsync(cancellationToken);

            //var supplierContacts = await supplierContactsQuaryable.Select(e => new OptionDto { Id = e.ProjectStakeholderId, Name = e.SupplierContact.FirstName + " " + e.SupplierContact.LastName + " - " + e.SupplierContact.EmailAddress1 + "(Supplier Contact)" })
            //                                                      .OrderBy(e => e.Name)
            //                                                      .ToListAsync(cancellationToken);

            var users = await usersQuaryable.Select(e => new OptionDto { Id = e.ProjectStakeholderId, Name = e.User.FirstName + " " + e.User.LastName + " - " + e.User.Email + "(User)" })
                                            .ToListAsync(cancellationToken);

            // Join all 4 lists to stakeHolders
            //var stakeHolders = regionContacts.Concat(storeContacts)
            //                                 .Concat(supplierContacts)
            //                                 .Concat(users)
            //                                 .ToList();

            var stakeHolders = users.ToList();

            return stakeHolders.ToList();

        }
        return new List<OptionDto>();
    }
}