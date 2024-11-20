namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStoreExternalUserVmQuery : IRequest<ProjectStoreExternalUserVm>
{
    public int ProjectId { get; set; }
    public int ProjectStakeholderId { get; set; }
}

public record ProjectStoreExternalUserVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreExternalUserVmQuery, ProjectStoreExternalUserVm>
{
    public async Task<ProjectStoreExternalUserVm> Handle(ProjectStoreExternalUserVmQuery query, CancellationToken cancellationToken)
    {
        ProjectStoreExternalUserVm userDetails = new();

        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == query.ProjectId, cancellationToken)
            ?? throw new Exception("No project found");

        var stakeholder = await Context.ProjectStakeholders.FirstOrDefaultAsync(e => e.ProjectStakeholderId == query.ProjectStakeholderId
                        && e.ProjectId == query.ProjectId, cancellationToken);

        if (stakeholder == null)
        {
            userDetails.IsStakeholder = false;
        }
        else
        {
            userDetails.IsStakeholder = true;
        }


        var assigned = await Context.ProjectAssignees
                                    .Where(e => e.ProjectId == query.ProjectId && e.ProjectStakeholderId == query.ProjectStakeholderId)
                                    .FirstOrDefaultAsync(cancellationToken);

        userDetails.IsAssigned = assigned != null;

        if (stakeholder is ProjectStakeholderUser)
        {
            var stakeholderUser = stakeholder as ProjectStakeholderUser;

            if (stakeholderUser != null)
            {
                var user = await Context.Users.FirstOrDefaultAsync(e => e.UserId == stakeholderUser.UserId, cancellationToken);
                userDetails.Id = user.UserId;
                userDetails.Name = user.FullName;
                userDetails.Email = user.Email;
            }
        }
        if (stakeholder is ProjectStakeholderExternalUser)
        {
            var stakeholderExternalUser = stakeholder as ProjectStakeholderExternalUser;

            if (stakeholderExternalUser != null)
            {
                var externalUser = await Context.ProjectExternalUsers
                                                .FirstOrDefaultAsync(e => e.ProjectExternalUserId == stakeholderExternalUser.ProjectExternalUserId, cancellationToken);

                userDetails.Id = externalUser.ProjectExternalUserId;
                userDetails.Name = $"{externalUser.FirstName} {externalUser.LastName}";
                userDetails.Email = externalUser.Email;
            }
        }
        if (stakeholder is ProjectStakeholderStoreContact)
        {
            var stakeholderStoreContact = stakeholder as ProjectStakeholderStoreContact;

            if (stakeholderStoreContact != null)
            {
                var storeContact = await Context.StoreContacts.FirstOrDefaultAsync(e => e.EntityContactId == stakeholderStoreContact.StoreContactId, cancellationToken);

                userDetails.Id = storeContact.EntityContactId;
                userDetails.Name = storeContact.FullName;
                userDetails.Email = storeContact.EmailAddress1;
            }
        }
        if (stakeholder is ProjectStakeholderSupplierContact)
        {
            if (stakeholder is ProjectStakeholderSupplierContact stakeholderSupplierContact)
            {
                var supplierContact = await Context.SupplierContacts.FirstOrDefaultAsync(e => e.EntityContactId == stakeholderSupplierContact.SupplierContactId, cancellationToken);

                userDetails.Id = supplierContact.EntityContactId;
                userDetails.Name = supplierContact.FullName;
                userDetails.Email = supplierContact.EmailAddress1;
            }
        }

        return userDetails;
    }
}