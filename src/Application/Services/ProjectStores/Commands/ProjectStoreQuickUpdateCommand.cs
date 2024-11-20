using Engage.Application.Services.ProjectProjectTags.Commands;

namespace Engage.Application.Services.ProjectStores.Commands;

public class ProjectStoreQuickUpdateCommand : IMapTo<ProjectStore>, IMapTo<ProjectTask>, IRequest<OperationStatus>
{
    public int Id { get; set; } // Project Id
    public int ProjectTaskId { get; set; }
    public string ProjectName { get; set; }
    public string TaskName { get; set; }
    public string TaskComment { get; set; }
    public int StoreId { get; set; }
    public int ProjectTypeId { get; set; }
    public int ProjectStatusId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? ProjectTaskTypeId { get; set; }
    public int? ProjectTaskPriorityId { get; set; }
    public List<int> ProjectTaskUserIds { get; set; } = [];
    public List<int> DcProductIds { get; set; } = [];
    public List<int> StoreAssetIds { get; set; } = [];
    public float? EstimatedHours { get; init; } = 0;
    public float? RemainingHours { get; init; } = 0;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreQuickUpdateCommand, ProjectStore>()
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ProjectName));

        profile.CreateMap<ProjectStoreQuickUpdateCommand, ProjectTask>()
               .ForMember(d => d.ProjectTaskId, opt => opt.MapFrom(s => s.ProjectTaskId))
               .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate))
               .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate))
               .ForMember(d => d.ProjectTaskTypeId, opt => opt.MapFrom(s => s.ProjectTaskTypeId))
               .ForMember(d => d.ProjectTaskPriorityId, opt => opt.MapFrom(s => s.ProjectTaskPriorityId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.TaskName));
    }
}
public record ProjectStoreQuickUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options) : IRequestHandler<ProjectStoreQuickUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreQuickUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.Id, cancellationToken);

        var task = await Context.ProjectTasks.FirstOrDefaultAsync(e => e.ProjectTaskId == command.ProjectTaskId, cancellationToken);

        if (entity == null || task == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        //mappedEntity.ProjectStatusId = (int)ProjectStatusId.New;
        //mappedEntity.ProjectPriorityId = (int)ProjectPriorityId.Default;

        //Context.ProjectStores.Add(mappedEntity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        //get the current project tags
        var currentTags = await Context.ProjectProjectTags.Where(e => e.ProjectId == entity.ProjectId)
                                                          .ToListAsync(cancellationToken);

        //get the different tag types
        var currentDCProducts = currentTags.OfType<ProjectProjectTagDCProduct>().ToList();
        var currentUsers = currentTags.OfType<ProjectProjectTagUser>().ToList();
        var currentAssets = currentTags.OfType<ProjectProjectTagStoreAsset>().ToList();

        //get the tags to delete
        var dcProductsToDelete = currentDCProducts.Where(e => !command.DcProductIds.Contains(e.DCProductId)).ToList();
        var usersToDelete = currentUsers.Where(e => !command.ProjectTaskUserIds.Contains(e.UserId)).ToList();
        var assetsToDelete = currentAssets.Where(e => !command.StoreAssetIds.Contains(e.StoreAssetId)).ToList();

        //get all the projectTagIds to delete
        var projectTagIdsToDelete = dcProductsToDelete.Select(e => e.ProjectProjectTagId)
                                                 .Union(usersToDelete.Select(e => e.ProjectProjectTagId))
                                                 .Union(assetsToDelete.Select(e => e.ProjectProjectTagId))
                                                 .ToList();

        var projectStakeholders = await Context.ProjectStakeholderUsers.Where(e => e.ProjectId == command.Id).Select(e => e.UserId).ToListAsync(cancellationToken);

        var usersToAdd = command.ProjectTaskUserIds.Where(e => !projectStakeholders.Contains(e)).ToList();

        if (usersToAdd.Count > 0)
        {
            Context.ProjectStakeholderUsers.AddRange(
            usersToAdd.Select(e => new ProjectStakeholderUser
            {
                ProjectId = command.Id,
                UserId = e
            }));
        }

        // delete tags
        if (projectTagIdsToDelete.Count > 0)
        {
            var currentTagsToDelete = currentTags.Where(e => projectTagIdsToDelete.Contains(e.ProjectProjectTagId)).ToList();
            Context.ProjectProjectTags.AddRange(currentTagsToDelete);
        }

        var tagCommand = new ProjectProjectTagInsertCommand
        {
            ProjectId = entity.ProjectId,
            ClaimIds = [],
            DCProductIds = command.DcProductIds,
            EmployeeJobTitleIds = [],
            EngageRegionIds = [],
            StoreIds = [command.StoreId],
            SupplierIds = [],
            UserIds = [],
            StoreAssetIds = command.StoreAssetIds
        };

        await Mediator.Send(tagCommand, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        #region assing stakholders to a project
        //if (opStatus.Status)
        //{
        //    //user stakeholders
        //    List<int> userIds = [];
        //    var user = await Context.Users.Where(e => e.Email.ToLower() == mappedEntity.CreatedBy.ToLower()).FirstOrDefaultAsync(cancellationToken);

        //    if (command.ProjectTaskUserIds.Any())
        //    {
        //        var assignedUsers = await Context.Users.Where(e => command.ProjectTaskUserIds.Contains(e.UserId)).Select(e => e.UserId).ToListAsync(cancellationToken);
        //        userIds.AddRange(assignedUsers);
        //        userIds.Distinct();
        //    }

        //    if (user != null)
        //    {
        //        userIds.Add(user.UserId);
        //    }

        //    // Add users based on call cycles
        //    var employeeUserIds = await Context.EmployeeStores.Where(e => e.StoreId == command.StoreId)
        //                                                      .Select(e => e.Employee.UserId)
        //                                                      .ToListAsync(cancellationToken);

        //    if (employeeUserIds.IsNotNullOrEmpty())
        //    {
        //        employeeUserIds = employeeUserIds.Distinct().ToList();
        //        foreach (var userId in employeeUserIds)
        //        {
        //            if (userId != null)
        //            {
        //                userIds.Add(userId.Value);
        //            }
        //        }
        //    }

        //    if (userIds.Count > 0)
        //    {
        //        userIds = userIds.Distinct().ToList();
        //        foreach (var userId in userIds)
        //        {
        //            var projectUser = new ProjectStakeholderUser
        //            {
        //                ProjectId = mappedEntity.ProjectId,
        //                UserId = userId,
        //            };
        //            Context.ProjectStakeholderUsers.Add(projectUser);
        //        }
        //    }

        //    var storeContactIds = await Context.StoreContacts.Where(e => e.StoreId == command.StoreId)
        //                                                     .Select(e => e.EntityContactId)
        //                                                     .ToListAsync(cancellationToken);

        //    if (storeContactIds != null && storeContactIds.Count > 0)
        //    {
        //        foreach (var storeContactId in storeContactIds)
        //        {
        //            var storeContact = new ProjectStakeholderStoreContact
        //            {
        //                ProjectId = mappedEntity.ProjectId,
        //                StoreContactId = storeContactId
        //            };

        //            Context.ProjectStakeholderStoreContacts.Add(storeContact);
        //        }
        //    }

        //    var store = await Context.Stores.Include(e => e.EngageRegion)
        //                                    .Where(e => e.StoreId == command.StoreId)
        //                                    .FirstOrDefaultAsync(cancellationToken);

        //    var regionContactsIds = await Context.EmployeeRegionContacts.Where(e => e.EngageRegionId == store.EngageRegionId)
        //                                                                .Select(e => e.EmployeeRegionContactId)
        //                                                                .ToListAsync(cancellationToken);

        //    if (regionContactsIds != null && regionContactsIds.Count > 0)
        //    {
        //        foreach (var regionContactId in regionContactsIds)
        //        {
        //            var regionContact = new ProjectStakeholderEmployeeRegionContact
        //            {
        //                ProjectId = mappedEntity.ProjectId,
        //                EmployeeRegionContactId = regionContactId
        //            };

        //            Context.ProjectStakeholderEmployeeRegionContacts.Add(regionContact);
        //        }
        //    }

        //    if (store != null)
        //    {
        //        Context.ProjectProjectTagStores.Add(new ProjectProjectTagStore
        //        {
        //            ProjectId = mappedEntity.ProjectId,
        //            StoreId = store.StoreId
        //        });
        //    }

        //    //assigned assets
        //    if (command.StoreAssetIds.Any())
        //    {
        //        var assets = await Context.StoreAssets.Where(e => command.StoreAssetIds.Contains(e.StoreAssetId)).ToListAsync(cancellationToken);

        //        var assetStakeholder = assets.Select(e => new ProjectProjectTagStoreAsset
        //        {
        //            StoreAssetId = e.StoreAssetId,
        //            ProjectId = mappedEntity.ProjectId
        //        });

        //        Context.ProjectProjectTagStoreAssets.AddRange(assetStakeholder);

        //    }

        //    //assign DcProducts
        //    if (command.DcProductIds.Any())
        //    {
        //        var products = await Context.DCProducts.Where(e => command.DcProductIds.Contains(e.DCProductId)).ToListAsync(cancellationToken);

        //        var productTags = products.Select(e => new ProjectProjectTagDCProduct
        //        {
        //            DCProductId = e.DCProductId,
        //            ProjectId = mappedEntity.ProjectId
        //        });

        //        Context.ProjectProjectTagDCProducts.AddRange(productTags);
        //    }
        //}
        #endregion

        if (opStatus.Status && command.TaskName.IsNotEmpty())
        {
            var mappedTask = Mapper.Map(task, command);

            if (command.ProjectTaskUserIds.Count > 0)
            {
                task.ProjectTaskStatusId = (int)ProjectTaskStatusId.Assigned;
            }

            var taskStatus = await Context.SaveChangesAsync(cancellationToken);

            if (taskStatus.Status && command.ProjectTaskUserIds.Count > 0)
            {
                var stakeholders = await Context.ProjectStakeholderUsers.Where(e => e.ProjectId == command.Id).ToListAsync(cancellationToken);

                var stakeHoldersAssigned = stakeholders.Where(e => command.ProjectTaskUserIds.Contains(e.ProjectStakeholderId)).Select(e => e.ProjectStakeholderId).ToList();

                var taskUsers = await Context.ProjectTaskProjectStakeholderUsers.Where(e => e.ProjectTaskId == command.ProjectTaskId).ToListAsync(cancellationToken);

                var usersToRemove = taskUsers.Where(e => !stakeHoldersAssigned.Contains(e.ProjectStakeholderId)).ToList();

                Context.ProjectTaskProjectStakeholderUsers.RemoveRange(usersToRemove);

                var assignedUsers = stakeHoldersAssigned.Select(e => new ProjectTaskProjectStakeholderUser
                {
                    ProjectTaskId = command.ProjectTaskId,
                    ProjectStakeholderId = e
                });

                Context.ProjectTaskProjectStakeholderUsers.AddRange(assignedUsers);
            }

            if (taskStatus.Status && command.TaskComment.IsNotEmpty())
            {
                ProjectTaskNote taskComment = new ProjectTaskNote
                {
                    ProjectTaskId = task.ProjectTaskId,
                    Note = command.TaskComment
                };

                Context.ProjectTaskNotes.Add(taskComment);

                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        opStatus.OperationId = mappedEntity.ProjectId;

        return opStatus;
    }
}
