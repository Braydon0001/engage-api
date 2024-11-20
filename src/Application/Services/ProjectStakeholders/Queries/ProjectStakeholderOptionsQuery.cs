namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class ProjectStakeholderOptionsQueryHandler : IRequestHandler<ProjectStakeholderOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ProjectStakeholderOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ProjectStakeholderOptionsQuery request, CancellationToken cancellationToken)
    {
        if (request.ProjectId != 0)
        {

            var users = await _context.ProjectStakeholderUsers.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.User)
                                                                 //.Select(e => new OptionDto(e.UserId, $"{e.User.FirstName} {e.User.LastName}"))
                                                                 .Select(e => new OptionDto(e.ProjectStakeholderId, $"{e.User.FirstName} {e.User.LastName}"))
                                                                 .ToListAsync(cancellationToken);

            var externalUsers = await _context.ProjectStakeholderExternalUsers.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.ProjectExternalUser)
                                                                 //.Select(e => new OptionDto(e.ProjectExternalUserId, $"{e.ProjectExternalUser.FirstName} {e.ProjectExternalUser.LastName}"))
                                                                 .Select(e => new OptionDto(e.ProjectStakeholderId, $"{e.ProjectExternalUser.FirstName} {e.ProjectExternalUser.LastName}"))
                                                                 .ToListAsync(cancellationToken);

            var storeContacts = await _context.ProjectStakeholderStoreContacts.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.StoreContact)
                                                                 //.Select(e => new OptionDto(e.StoreContactId, $"{e.StoreContact.FirstName} {e.StoreContact.LastName}"))
                                                                 .Select(e => new OptionDto(e.ProjectStakeholderId, $"{e.StoreContact.FirstName} {e.StoreContact.LastName}"))
                                                                 .ToListAsync(cancellationToken);

            var supplierContacts = await _context.ProjectStakeholderSupplierContacts.Where(e => e.ProjectId == request.ProjectId)
                                                                 .Include(e => e.SupplierContact)
                                                                 //.Select(e => new OptionDto(e.SupplierContactId, $"{e.SupplierContact.FirstName} {e.SupplierContact.LastName}"))
                                                                 .Select(e => new OptionDto(e.ProjectStakeholderId, $"{e.SupplierContact.FirstName} {e.SupplierContact.LastName}"))
                                                                 .ToListAsync(cancellationToken);



            return new List<OptionDto>([.. users, .. externalUsers, .. storeContacts, .. supplierContacts]);

        }
        return new List<OptionDto>();
    }
}