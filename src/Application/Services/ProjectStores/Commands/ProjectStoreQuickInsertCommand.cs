namespace Engage.Application.Services.ProjectStores.Commands;

public class ProjectStoreQuickInsertCommand : IMapTo<ProjectStore>, IRequest<OperationStatus>
{
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
    public List<int> ProjectTaskUserIds { get; set; }
    public List<int> DcProductIds { get; set; }
    public List<int> StoreAssetIds { get; set; }
    public float? EstimatedHours { get; init; } = 0;
    public float? RemainingHours { get; init; } = 0;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreQuickInsertCommand, ProjectStore>()
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ProjectName));
    }
}
public record ProjectStoreQuickInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options) : IRequestHandler<ProjectStoreQuickInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreQuickInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectStoreQuickInsertCommand, ProjectStore>(command);

        entity.ProjectStatusId = (int)ProjectStatusId.Unassigned;
        entity.ProjectPriorityId = (int)ProjectPriorityId.Default;

        Context.ProjectStores.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        #region assing stakholders to a project
        if (opStatus.Status)
        {
            //user stakeholders
            List<int> userIds = [];
            var user = await Context.Users.Where(e => e.Email.ToLower() == entity.CreatedBy.ToLower()).FirstOrDefaultAsync(cancellationToken);

            if (command.ProjectTaskUserIds.Any())
            {
                var assignedUsers = await Context.Users.Where(e => command.ProjectTaskUserIds.Contains(e.UserId)).Select(e => e.UserId).ToListAsync(cancellationToken);
                userIds.AddRange(assignedUsers);
                userIds.Distinct();
            }

            if (user != null)
            {
                userIds.Add(user.UserId);
            }

            // Add users based on call cycles
            var employeeUserIds = await Context.EmployeeStores.Where(e => e.StoreId == command.StoreId)
                                                              .Select(e => e.Employee.UserId)
                                                              .ToListAsync(cancellationToken);

            if (employeeUserIds.IsNotNullOrEmpty())
            {
                employeeUserIds = employeeUserIds.Distinct().ToList();
                foreach (var userId in employeeUserIds)
                {
                    if (userId != null)
                    {
                        userIds.Add(userId.Value);
                    }
                }
            }

            if (userIds.Count > 0)
            {
                userIds = userIds.Distinct().ToList();
                foreach (var userId in userIds)
                {
                    var projectUser = new ProjectStakeholderUser
                    {
                        ProjectId = entity.ProjectId,
                        UserId = userId,
                    };
                    Context.ProjectStakeholderUsers.Add(projectUser);
                }
            }

            var storeContactIds = await Context.StoreContacts.Where(e => e.StoreId == command.StoreId)
                                                             .Select(e => e.EntityContactId)
                                                             .ToListAsync(cancellationToken);

            if (storeContactIds != null && storeContactIds.Count > 0)
            {
                foreach (var storeContactId in storeContactIds)
                {
                    var storeContact = new ProjectStakeholderStoreContact
                    {
                        ProjectId = entity.ProjectId,
                        StoreContactId = storeContactId
                    };

                    Context.ProjectStakeholderStoreContacts.Add(storeContact);
                }
            }

            var store = await Context.Stores.Include(e => e.EngageRegion)
                                            .Where(e => e.StoreId == command.StoreId)
                                            .FirstOrDefaultAsync(cancellationToken);

            var regionContactsIds = await Context.EmployeeRegionContacts.Where(e => e.EngageRegionId == store.EngageRegionId)
                                                                        .Select(e => e.EmployeeRegionContactId)
                                                                        .ToListAsync(cancellationToken);

            if (regionContactsIds != null && regionContactsIds.Count > 0)
            {
                foreach (var regionContactId in regionContactsIds)
                {
                    var regionContact = new ProjectStakeholderEmployeeRegionContact
                    {
                        ProjectId = entity.ProjectId,
                        EmployeeRegionContactId = regionContactId
                    };

                    Context.ProjectStakeholderEmployeeRegionContacts.Add(regionContact);
                }
            }

            if (store != null)
            {
                Context.ProjectProjectTagStores.Add(new ProjectProjectTagStore
                {
                    ProjectId = entity.ProjectId,
                    StoreId = store.StoreId
                });
            }

            //assigned assets
            if (command.StoreAssetIds.Any())
            {
                var assets = await Context.StoreAssets.Where(e => command.StoreAssetIds.Contains(e.StoreAssetId)).ToListAsync(cancellationToken);

                var assetStakeholder = assets.Select(e => new ProjectProjectTagStoreAsset
                {
                    StoreAssetId = e.StoreAssetId,
                    ProjectId = entity.ProjectId
                });

                Context.ProjectProjectTagStoreAssets.AddRange(assetStakeholder);

            }

            //assign DcProducts
            if (command.DcProductIds.Any())
            {
                var products = await Context.DCProducts.Where(e => command.DcProductIds.Contains(e.DCProductId)).ToListAsync(cancellationToken);

                var productTags = products.Select(e => new ProjectProjectTagDCProduct
                {
                    DCProductId = e.DCProductId,
                    ProjectId = entity.ProjectId
                });

                Context.ProjectProjectTagDCProducts.AddRange(productTags);
            }
        }
        #endregion

        if (opStatus.Status && command.TaskName.IsNotEmpty())
        {
            ProjectTask task = new ProjectTask
            {
                Name = command.TaskName,
                ProjectId = entity.ProjectId,
                ProjectTaskStatusId = command.ProjectTaskUserIds.Any() ? (int)ProjectTaskStatusId.Assigned : (int)ProjectTaskStatusId.Open,
                ProjectTaskPriorityId = command.ProjectTaskPriorityId.HasValue ? command.ProjectTaskPriorityId.Value : (int)ProjectTaskPriorityId.Default,
                ProjectTaskTypeId = command.ProjectTaskTypeId,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
            };

            Context.ProjectTasks.Add(task);

            var taskStatus = await Context.SaveChangesAsync(cancellationToken);

            if (taskStatus.Status && command.ProjectTaskUserIds.Count > 0)
            {
                var stakeholders = await Context.ProjectStakeholderUsers.Where(e => e.ProjectId == entity.ProjectId).ToListAsync(cancellationToken);
                var assignedUsers = command.ProjectTaskUserIds.Select(e => new ProjectTaskProjectStakeholderUser
                {
                    ProjectStakeholderId = stakeholders.FirstOrDefault(f => f.UserId == e).ProjectStakeholderId,
                    ProjectTaskId = task.ProjectTaskId
                });
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

        opStatus.OperationId = entity.ProjectId;

        return opStatus;
    }
}
