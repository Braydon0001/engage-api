namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderIdentifierOptionsQuery : GetQuery, IRequest<List<ProjectStakeholderSearchOption>>
{
    public int ProjectId { get; set; }
}



public class ProjectStakeholderIdentifierOptionsHandler : IRequestHandler<ProjectStakeholderIdentifierOptionsQuery, List<ProjectStakeholderSearchOption>>
{
    private readonly IAppDbContext _context;

    public ProjectStakeholderIdentifierOptionsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectStakeholderSearchOption>> Handle(ProjectStakeholderIdentifierOptionsQuery request, CancellationToken cancellationToken)
    {
        if (request.ProjectId != 0)
        {

            var users = await _context.ProjectStakeholderUsers.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.User)
                                                                 //.Select(e => new OptionDto(e.UserId, $"{e.User.FirstName} {e.User.LastName}"))
                                                                 .Select(e => new ProjectStakeholderSearchOption
                                                                 {
                                                                     Id = e.UserId,
                                                                     Name = $"{e.User.FirstName} {e.User.LastName}",
                                                                     Identifier = "user"
                                                                 })
                                                                 .ToListAsync(cancellationToken);

            var externalUsers = await _context.ProjectStakeholderExternalUsers.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.ProjectExternalUser)
                                                                 //.Select(e => new OptionDto(e.ProjectExternalUserId, $"{e.ProjectExternalUser.FirstName} {e.ProjectExternalUser.LastName}"))
                                                                 .Select(e => new ProjectStakeholderSearchOption
                                                                 {
                                                                     Id = e.ProjectExternalUserId,
                                                                     Name = $"{e.ProjectExternalUser.FirstName} {e.ProjectExternalUser.LastName}",
                                                                     Identifier = "externalUser"
                                                                 })
                                                                 .ToListAsync(cancellationToken);

            var storeContacts = await _context.ProjectStakeholderStoreContacts.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.StoreContact)
                                                                 //.Select(e => new OptionDto(e.StoreContactId, $"{e.StoreContact.FirstName} {e.StoreContact.LastName}"))
                                                                 .Select(e => new ProjectStakeholderSearchOption
                                                                 {
                                                                     Id = e.StoreContactId,
                                                                     Name = $"{e.StoreContact.FirstName} {e.StoreContact.LastName}",
                                                                     Identifier = "storeContact"
                                                                 })
                                                                 .ToListAsync(cancellationToken);

            var supplierContacts = await _context.ProjectStakeholderSupplierContacts.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.SupplierContact)
                                                                 //.Select(e => new OptionDto(e.SupplierContactId, $"{e.SupplierContact.FirstName} {e.SupplierContact.LastName}"))
                                                                 .Select(e => new ProjectStakeholderSearchOption
                                                                 {
                                                                     Id = e.SupplierContactId,
                                                                     Name = $"{e.SupplierContact.FirstName} {e.SupplierContact.LastName}",
                                                                     Identifier = "supplierContact"
                                                                 })
                                                                 .ToListAsync(cancellationToken);



            return new List<ProjectStakeholderSearchOption>([.. users, .. externalUsers, .. storeContacts, .. supplierContacts]);

        }
        return new List<ProjectStakeholderSearchOption>();
    }
}