using Engage.Application.Services.DCProducts.Queries;
using Engage.Application.Services.EngageBrands.Queries;
using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectStakeholders.Queries;
using Engage.Application.Services.ProjectStoreAssets.Queries;
using Engage.Application.Services.ProjectSuppliers.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public record ProjectStoreVmQuery(int Id, bool IsExternal = false) : IRequest<ProjectStoreVm>;

public class ProjectStoreVmHandler : BaseQueryHandler, IRequestHandler<ProjectStoreVmQuery, ProjectStoreVm>
{
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    public ProjectStoreVmHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user) : base(context, mapper)
    {
        _mediator = mediator;
        _user = user;
    }

    public async Task<ProjectStoreVm> Handle(ProjectStoreVmQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var queryable = _context.ProjectStores.IgnoreQueryFilters().Where(e => e.Deleted == false).AsQueryable().AsNoTracking();

            queryable = queryable.Include(e => e.Store)
                                 .Include(e => e.Owner)
                                 .Include(e => e.ProjectType)
                                 .Include(e => e.ProjectSubType).ThenInclude(e => e.ProjectType)
                                 .Include(e => e.ProjectStatus)
                                 .Include(e => e.ProjectPriority)
                                 .Include(e => e.ProjectCategory)
                                 .Include(e => e.ProjectSubCategory).ThenInclude(e => e.ProjectCategory);

            var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectId == query.Id, cancellationToken);

            var mappedEntity = _mapper.Map<ProjectStoreVm>(entity);

            if (mappedEntity != null)
            {
                // Assigned users
                var assigned = await _context.ProjectAssignees.AsNoTracking()
                                                             .Where(e => e.ProjectId == query.Id)
                                                             .Include(e => e.ProjectStakeholder)
                                                             .Select(e => e.ProjectStakeholder)
                                                             .ToListAsync(cancellationToken);

                var usersAssigned = assigned.OfType<ProjectStakeholderUser>().Select(e => e.ProjectStakeholderId).ToList();
                var externalUserAssigned = assigned.OfType<ProjectStakeholderExternalUser>().Select(e => e.ProjectStakeholderId).ToList();
                var storeContactAssigned = assigned.OfType<ProjectStakeholderStoreContact>().Select(e => e.ProjectStakeholderId).ToList();
                var supplierContactAssigned = assigned.OfType<ProjectStakeholderSupplierContact>().Select(e => e.ProjectStakeholderId).ToList();
                var regionContactAssigned = assigned.OfType<ProjectStakeholderEmployeeRegionContact>().Select(e => e.ProjectStakeholderId).ToList();

                //TODO: change to userId, ExternalStakeholderId, ect
                //var usersAssigned = assigned.OfType<ProjectStakeholderUser>().Select(e => e.ProjectStakeholderId).ToList();
                //var externalUserAssigned = assigned.OfType<ProjectStakeholderExternalUser>().Select(e => e.ProjectStakeholderId).ToList();
                //var storeContactAssigned = assigned.OfType<ProjectStakeholderStoreContact>().Select(e => e.ProjectStakeholderId).ToList();
                //var supplierContactAssigned = assigned.OfType<ProjectStakeholderSupplierContact>().Select(e => e.ProjectStakeholderId).ToList();
                //var regionContactAssigned = assigned.OfType<ProjectStakeholderEmployeeRegionContact>().Select(e => e.ProjectStakeholderId).ToList();

                //var userOptions = await _context.Users.AsNoTracking()
                //                                     .Where(e => usersAssigned.Contains(e.UserId))
                //                                     .Select(e => new OptionDto { Id = e.UserId, Name = $"{e.FirstName} {e.LastName}" })
                //                                     .ToListAsync(cancellationToken);

                //var externalUserOptions = await _context.ProjectExternalUsers.AsNoTracking()
                //                                                            .Where(e => externalUserAssigned.Contains(e.ProjectExternalUserId))
                //                                                            .Select(e => new OptionDto { Id = e.ProjectExternalUserId, Name = $"{e.FirstName} {e.LastName}" })
                //                                                            .ToListAsync(cancellationToken);

                //var storeContactOptions = await _context.StoreContacts.Where(e => storeContactAssigned.Contains(e.EntityContactId))
                //                                                                                     .Select(e => new OptionDto { Id = e.EntityContactId, Name = $"{e.FirstName} {e.LastName}" })
                //                                                                                     .ToListAsync(cancellationToken);

                //var supplierContactOptions = await _context.SupplierContacts.Where(e => supplierContactAssigned.Contains(e.EntityContactId))
                //                                                                                              .Select(e => new OptionDto { Id = e.EntityContactId, Name = $"{e.FirstName} {e.LastName}" })
                //                                                                                              .ToListAsync(cancellationToken);

                var userOptions = await _context.ProjectStakeholderUsers.AsNoTracking()
                                                     .Where(e => usersAssigned.Contains(e.ProjectStakeholderId))
                                                     .Select(e => new ProjectStakeholderSearchOption { Id = e.UserId, Name = $"{e.User.FirstName} {e.User.LastName}", Identifier = "user" })
                                                     .ToListAsync(cancellationToken);

                var externalUserOptions = await _context.ProjectStakeholderExternalUsers.AsNoTracking()
                                                                            .Where(e => externalUserAssigned.Contains(e.ProjectStakeholderId))
                                                                            .Select(e => new ProjectStakeholderSearchOption { Id = e.ProjectExternalUserId, Name = $"{e.ProjectExternalUser.FirstName} {e.ProjectExternalUser.LastName}", Identifier = "externalUser" })
                                                                            .ToListAsync(cancellationToken);

                var storeContactOptions = await _context.ProjectStakeholderStoreContacts.Where(e => storeContactAssigned.Contains(e.ProjectStakeholderId))
                                                                                                     .Select(e => new ProjectStakeholderSearchOption { Id = e.StoreContactId, Name = $"{e.StoreContact.FirstName} {e.StoreContact.LastName}", Identifier = "storeContact" })
                                                                                                     .ToListAsync(cancellationToken);

                var supplierContactOptions = await _context.ProjectStakeholderSupplierContacts.Where(e => supplierContactAssigned.Contains(e.ProjectStakeholderId))
                                                                                                              .Select(e => new ProjectStakeholderSearchOption
                                                                                                              { Id = e.SupplierContactId, Name = $"{e.SupplierContact.FirstName} {e.SupplierContact.LastName}", Identifier = "supplierContact" })
                                                                                                              .ToListAsync(cancellationToken);

                mappedEntity.ProjectAssignedTo = [.. userOptions, .. externalUserOptions, .. storeContactOptions, .. supplierContactOptions];

                var products = await _context.ProjectDcProducts.AsNoTracking()
                                                              .Where(e => e.ProjectId == query.Id)
                                                              .Include(e => e.DcProduct)
                                                              .Select(e => e.DcProduct)
                                                              .ProjectTo<DCProductOption>(_mapper.ConfigurationProvider)
                                                              .ToListAsync(cancellationToken);

                mappedEntity.DcProductIds = products;

                var suppliers = await _context.ProjectSuppliers.AsNoTracking()
                                                              .Where(e => e.ProjectId == query.Id)
                                                              .ProjectTo<ProjectSupplierOption>(_mapper.ConfigurationProvider)
                                                              .ToListAsync(cancellationToken);

                mappedEntity.SupplierIds = suppliers;

                var engageBrands = await _context.ProjectEngageBrands.AsNoTracking()
                                                                     .Where(e => e.ProjectId == query.Id)
                                                                     .Include(e => e.EngageBrand)
                                                                     .Select(e => e.EngageBrand)
                                                                     .ProjectTo<EngageBrandOption>(_mapper.ConfigurationProvider)
                                                                     .ToListAsync(cancellationToken);

                mappedEntity.EngageBrandIds = engageBrands;

                var assets = await _context.ProjectStoreAssets.AsNoTracking()
                                                             .Where(e => e.ProjectId == query.Id)
                                                             .Include(e => e.StoreAsset)
                                                             .ProjectTo<ProjectStoreAssetOption>(_mapper.ConfigurationProvider)
                                                             .ToListAsync(cancellationToken);

                mappedEntity.StoreAssetIds = assets;

                if (_user.UserName.IsNullOrWhiteSpace())
                {
                    mappedEntity.IsOwner = false;
                    mappedEntity.IsAssigned = false;
                }
                else
                {
                    var currentUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Email.ToLower() == _user.UserName.ToLower(), cancellationToken);

                    var assignedUsersIds = await _context.ProjectStakeholderUsers.AsNoTracking()
                                                     .Where(e => usersAssigned.Contains(e.ProjectStakeholderId))
                                                     .Select(e => e.UserId)
                                                     .ToListAsync(cancellationToken);
                    if (currentUser == null)
                    {
                        mappedEntity.IsOwner = false;
                        mappedEntity.IsAssigned = false;
                    }

                    if (entity.OwnerId == currentUser.UserId)
                    {
                        mappedEntity.IsOwner = true;
                    }

                    if (assignedUsersIds.Any(e => e == currentUser.UserId))
                    {
                        mappedEntity.IsAssigned = true;
                    }
                }

            }

            if (!query.IsExternal)
            {
                var stakeholders = await _mediator.Send(new ProjectStakeholderIdentifierOptionsQuery
                {
                    ProjectId = mappedEntity.Id
                }, cancellationToken);

                mappedEntity.ProjectStakeholderIds = stakeholders;
                mappedEntity.StakeholderIds = stakeholders;
            }
            else
            {
                mappedEntity.ProjectStakeholderIds = null;
            }


            return entity == null ? null : mappedEntity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}