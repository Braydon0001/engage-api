namespace Engage.Application.Services.ProjectStakeholders.Queries;

public class ProjectStakeholderMobileSearchOptionsOfflineQuery : IRequest<List<ProjectStakeholderSearchOption>>
{
    public int? StoreId { get; set; }
    public string SupplierIds { get; set; }
    public string Search { get; set; }
    public int? userId { get; set; }
}

public record ProjectStakeholderMobileSearchOptionsOfflineHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStakeholderMobileSearchOptionsOfflineQuery, List<ProjectStakeholderSearchOption>>
{
    public async Task<List<ProjectStakeholderSearchOption>> Handle(ProjectStakeholderMobileSearchOptionsOfflineQuery query, CancellationToken cancellationToken)
    {
        List<ProjectStakeholderSearchOption> returnData = [];

        var userStakeholderIds = await Context.ProjectStakeholderUsers.Where(e => e.UserId == query.userId && e.Disabled != true).Select(e => e.ProjectStakeholderId).ToListAsync(cancellationToken);





        var projectIds = await Context.ProjectAssignees.AsNoTracking()
                                                       .Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                       .Include(e => e.ProjectStakeholder)
                                                       .Select(e => e.ProjectId)
                                                       .ToListAsync(cancellationToken);

        var ownerProjectIds = await Context.ProjectStores.Where(e => e.OwnerId == query.userId).Select(e => e.ProjectId).ToListAsync(cancellationToken);


        projectIds.AddRange(ownerProjectIds);


        var stakeholders = await Context.ProjectStakeholders.Where(e => projectIds.Contains(e.ProjectId)).ToListAsync(cancellationToken);


        var stakeholderUsers = stakeholders.OfType<ProjectStakeholderUser>().Select(e => e.UserId).ToList();
        var externalUserAssigned = stakeholders.OfType<ProjectStakeholderExternalUser>().Select(e => e.ProjectExternalUserId).ToList();
        var storeContactAssigned = stakeholders.OfType<ProjectStakeholderStoreContact>().Select(e => e.StoreContactId).ToList();
        var supplierContactAssigned = stakeholders.OfType<ProjectStakeholderSupplierContact>().Select(e => e.SupplierContactId).ToList();

        var userOptions = await Context.Users.Where(e => stakeholderUsers.Contains(e.UserId))
            .Select(e => new ProjectStakeholderSearchOption
            {
                Id = e.UserId,
                Name = $"{e.FullName} - {e.Email}",
                Identifier = "user"

            }).ToListAsync(cancellationToken);



        var externalUserOptions = await Context.ProjectExternalUsers.Where(e => externalUserAssigned.Contains(e.ProjectExternalUserId))
           .Select(e => new ProjectStakeholderSearchOption
           {
               Id = e.ProjectExternalUserId,
               Name = $"{e.FirstName} {e.LastName} - {e.Email}",
               Identifier = "externalUser"

           }).ToListAsync(cancellationToken);


        var storeUserOptions = await Context.StoreContacts.Where(e => storeContactAssigned.Contains(e.EntityContactId))
          .Select(e => new ProjectStakeholderSearchOption
          {
              Id = e.EntityContactId,
              Name = $"{e.FullName} - {e.EmailAddress1}",
              Identifier = "storeContact"

          }).ToListAsync(cancellationToken);

        var supplierUserOptions = await Context.SupplierContacts.Where(e => supplierContactAssigned.Contains(e.EntityContactId))
           .Select(e => new ProjectStakeholderSearchOption
           {
               Id = e.EntityContactId,
               Name = $"{e.FullName} - {e.EmailAddress1}",
               Identifier = "supplierContact"

           }).ToListAsync(cancellationToken);


        returnData.AddRange([.. userOptions, .. externalUserOptions, .. storeUserOptions, .. supplierUserOptions]);



        return returnData;
    }
}