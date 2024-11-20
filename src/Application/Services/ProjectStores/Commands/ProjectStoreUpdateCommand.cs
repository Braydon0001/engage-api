using Engage.Application.Services.ProjectAssignees.Commands;
using Engage.Application.Services.ProjectDcProducts.Commands;
using Engage.Application.Services.ProjectEngageBrands.Commands;
using Engage.Application.Services.ProjectStakeholders.Commands;
using Engage.Application.Services.ProjectStoreAssets.Commands;
using Engage.Application.Services.ProjectSuppliers.Commands;

namespace Engage.Application.Services.ProjectStores.Commands;

public class ProjectStoreUpdateCommand : IMapTo<ProjectStore>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Description { get; init; }
    public int ProjectStoreId { get; init; }
    public int ProjectTypeId { get; init; }
    public int ProjectSubTypeId { get; set; }
    public List<int> DcProductIds { get; set; }
    public List<int> StoreAssetIds { get; set; }
    public List<int> SupplierIds { get; set; }
    public List<int> EngageBrandIds { get; set; }
    public List<StakeholderIds> ProjectAssignedTo { get; set; }
    public List<StakeholderIds> ProjectStakeholderIds { get; set; }
    public int ProjectCategoryId { get; set; }
    public int? ProjectSubCategoryId { get; set; }
    public int ProjectPriorityId { get; set; }
    public string ProjectStatus { get; set; }
    public string ProjectComment { get; set; }
    public string ProjectStatusId { get; set; }
    public DateTime? EndDate { get; init; }
    public DateTime? ClosedDate { get; init; }
    public bool SendCommunication { get; set; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreUpdateCommand, ProjectStore>()
               .ForMember(s => s.Name, opt => opt.MapFrom(s => s.Description))
               .ForMember(s => s.StoreId, opt => opt.MapFrom(s => s.ProjectStoreId))
               .ForMember(s => s.ProjectStatusId, opt => opt.Ignore())
               .ForMember(s => s.ProjectStatus, opt => opt.Ignore())
               .ForMember(s => s.ClosedDate, opt => opt.Ignore());
    }
}

public record ProjectStoreUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStoreUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectStores.SingleOrDefaultAsync(e => e.ProjectId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        var opstatus = new OperationStatus();
        ProjectComment comment = null;

        if (int.TryParse(command.ProjectStatus, out int statusId))
        {
            var statusChanged = statusId != entity.ProjectStatusId;
            if (statusId != entity.ProjectStatusId && command.ProjectComment.IsNullOrEmpty())
            {
                throw new Exception("Error no comment found");

            }
            if (statusChanged && statusId == (int)ProjectStatusId.Completed)
            {
                Context.projectStatusHistories.Add(new ProjectStatusHistory
                {
                    ProjectId = entity.ProjectId,
                    ProjectStatusId = entity.ProjectStatusId,
                    Reason = command.ProjectComment
                });
                mappedEntity.ProjectStatusId = statusId;
                mappedEntity.ClosedDate = command.ClosedDate ?? DateTime.Now.AddHours(2); //GMT +2
            }
            if (statusChanged && statusId == (int)ProjectStatusId.Unassigned)
            {
                Context.projectStatusHistories.Add(new ProjectStatusHistory
                {
                    ProjectId = entity.ProjectId,
                    ProjectStatusId = entity.ProjectStatusId,
                    Reason = command.ProjectComment
                });
                mappedEntity.ProjectStatusId = statusId;
            }
            if (statusChanged && statusId == (int)ProjectStatusId.Assigned)
            {
                Context.projectStatusHistories.Add(new ProjectStatusHistory
                {
                    ProjectId = entity.ProjectId,
                    ProjectStatusId = entity.ProjectStatusId,
                    Reason = command.ProjectComment
                });
                mappedEntity.ProjectStatusId = statusId;
                //mappedEntity.ClosedDate = null;
            }

            if (command.ProjectComment.IsNotNullOrEmpty())
            {
                comment = new ProjectComment
                {
                    ProjectId = entity.ProjectId,
                    ProjectStatusId = statusId,
                    Comment = command.ProjectComment
                };
                Context.ProjectComments.Add(comment);

                //await Context.SaveChangesAsync(cancellationToken);

                //opstatus.ReturnObject = comment.ProjectCommentId;
            }
        }

        if (command.StoreAssetIds.IsNotNull())
        {
            await Mediator.Send(new ProjectStoreUpdateAssetCommand
            {
                ProjectId = command.Id,
                StoreAssetIds = command.StoreAssetIds,
                Save = false
            }, cancellationToken);
        }

        if (command.SupplierIds.IsNotNull())
        {
            await Mediator.Send(new ProjectUpdateSupplierCommand
            {
                ProjectId = command.Id,
                SupplierIds = command.SupplierIds,
                Save = false
            }, cancellationToken);
        }

        if (command.DcProductIds.IsNotNull())
        {
            await Mediator.Send(new ProjectUpdateDcProductCommand
            {
                ProjectId = command.Id,
                DcProductIds = command.DcProductIds,
                Save = false
            }, cancellationToken);
        }

        if (command.ProjectAssignedTo.IsNotNull())
        {
            await Mediator.Send(new ProjectAssigneeWebUpdateCommand
            {
                ProjectId = command.Id,
                ProjectAssignedIds = command.ProjectAssignedTo,
            }, cancellationToken);
        }

        if (command.EngageBrandIds.IsNotNull())
        {
            await Mediator.Send(new ProjectEngageBrandUpdateCommand
            {
                ProjectId = entity.ProjectId,
                EngageBrandIds = command.EngageBrandIds,
                Save = false
            }, cancellationToken);
        }

        if (command.ProjectStakeholderIds.IsNotNull())
        {
            await Mediator.Send(new ProjectStakeholderUpdateCommand
            {
                ProjectId = entity.ProjectId,
                ProjectStakeholderIds = command.ProjectStakeholderIds,
                Save = false
            });
        }

        //mobile stakeholder update
        //if (command.ProjectStakeholderIds.IsNotNullOrEmpty())
        //{
        //    var assignees = await Context.ProjectAssignees.Where(e => e.ProjectId == command.Id).ToListAsync(cancellationToken);

        //    var stakeholders = await Context.ProjectStakeholders.Where(e => e.ProjectId == command.Id).ToListAsync(cancellationToken);

        //    var users = stakeholders.OfType<ProjectStakeholderUser>().ToList();
        //    var external = stakeholders.OfType<ProjectStakeholderExternalUser>().ToList();
        //    var storeContact = stakeholders.OfType<ProjectStakeholderStoreContact>().ToList();
        //    var supplierContact = stakeholders.OfType<ProjectStakeholderSupplierContact>().ToList();

        //    var assignedUsers = users.Where(e => assignees.Select(e => e.ProjectStakeholderId).Contains(e.ProjectStakeholderId)).ToList();
        //    var assignedExternal = external.Where(e => assignees.Select(e => e.ProjectStakeholderId).Contains(e.ProjectStakeholderId)).ToList();
        //    var assignedStoreContact = storeContact.Where(e => assignees.Select(e => e.ProjectStakeholderId).Contains(e.ProjectStakeholderId)).ToList();
        //    var assignedSupplierContact = supplierContact.Where(e => assignees.Select(e => e.ProjectStakeholderId).Contains(e.ProjectStakeholderId)).ToList();

        //    if (!command.ProjectStakeholderIds.Any(e => e.Identifier == "user" && e.Id == entity.OwnerId))
        //    {
        //        throw new Exception("Cannot remove owner as a stakeholder");
        //    }

        //    var assignedUserRemoved = assignedUsers.Where(e =>
        //                                                    !command.ProjectStakeholderIds.Where(f => f.Identifier == "user")
        //                                                                                  .Select(f => f.Id)
        //                                                                                  .Contains(e.UserId))
        //                                           .Any();

        //    if (!assignedUserRemoved)
        //    {
        //        assignedUserRemoved = assignedExternal.Where(e =>
        //                                        !command.ProjectStakeholderIds.Where(f => f.Identifier == "externalUser").Select(f => f.Id).Contains(e.ProjectExternalUserId))
        //                                              .Any();

        //    }

        //    if (!assignedUserRemoved)
        //    {
        //        assignedUserRemoved = assignedStoreContact.Where(e =>
        //                                                    !command.ProjectStakeholderIds.Where(f => f.Identifier == "storeContact")
        //                                                                                  .Select(f => f.Id)
        //                                                                                  .Contains(e.StoreContactId))
        //                                           .Any();
        //    }

        //    if (!assignedUserRemoved)
        //    {
        //        assignedUserRemoved = assignedSupplierContact.Where(e =>
        //                                                    !command.ProjectStakeholderIds.Where(f => f.Identifier == "supplierContact")
        //                                                                                  .Select(f => f.Id)
        //                                                                                  .Contains(e.SupplierContactId))
        //                                           .Any();
        //    }

        //    if (assignedUserRemoved)
        //    {
        //        throw new Exception("Cannot remove assigned users from stakeholder list");
        //    }

        //    var usersToRemove = users.Where(e => !command.ProjectStakeholderIds.Where(e => e.Identifier == "user").Select(e => e.Id).Contains(e.UserId));
        //    var externalUsersToRemove = external.Where(e => !command.ProjectStakeholderIds.Where(e => e.Identifier == "externalUser").Select(e => e.Id).Contains(e.ProjectExternalUserId));
        //    var storeContactsToRemove = storeContact.Where(e => !command.ProjectStakeholderIds.Where(e => e.Identifier == "storeContact").Select(e => e.Id).Contains(e.StoreContactId));
        //    var supplierContactsToRemove = supplierContact.Where(e => !command.ProjectStakeholderIds.Where(e => e.Identifier == "supplierCon").Select(e => e.Id).Contains(e.SupplierContactId));

        //    Context.ProjectStakeholderUsers.RemoveRange(usersToRemove);
        //    Context.ProjectStakeholderExternalUsers.RemoveRange(externalUsersToRemove);
        //    Context.ProjectStakeholderStoreContacts.RemoveRange(storeContactsToRemove);
        //    Context.ProjectStakeholderSupplierContacts.RemoveRange(supplierContactsToRemove);


        //    //save new stakeholders and remove old ones
        //    var newUserStakeholders = command.ProjectStakeholderIds.Where(e => e.Identifier == "user" && !users.Select(e => e.UserId).Contains(e.Id)).Select(e => new ProjectStakeholderUser
        //    {
        //        ProjectId = mappedEntity.ProjectId,
        //        UserId = e.Id
        //    }).ToList();

        //    var newExternalUsers = command.ProjectStakeholderIds.Where(e => e.Identifier == "externalUser" && !external.Select(e => e.ProjectExternalUserId).Contains(e.Id)).Select(e => new ProjectStakeholderExternalUser
        //    {
        //        ProjectId = mappedEntity.ProjectId,
        //        ProjectExternalUserId = e.Id
        //    }).ToList();

        //    var newStoreContacts = command.ProjectStakeholderIds.Where(e => e.Identifier == "storeContact" && !storeContact.Select(e => e.StoreContactId).Contains(e.Id)).Select(e => new ProjectStakeholderStoreContact
        //    {
        //        ProjectId = mappedEntity.ProjectId,
        //        StoreContactId = e.Id
        //    }).ToList();

        //    var newSupplierContacts = command.ProjectStakeholderIds.Where(e => e.Identifier == "supplierContact" && !supplierContact.Select(e => e.SupplierContactId).Contains(e.Id)).Select(e => new ProjectStakeholderSupplierContact
        //    {
        //        ProjectId = mappedEntity.ProjectId,
        //        SupplierContactId = e.Id
        //    }).ToList();

        //    Context.ProjectStakeholderUsers.AddRange(newUserStakeholders);
        //    Context.ProjectStakeholderExternalUsers.AddRange(newExternalUsers);
        //    Context.ProjectStakeholderStoreContacts.AddRange(newStoreContacts);
        //    Context.ProjectStakeholderSupplierContacts.AddRange(newSupplierContacts);
        //}

        await Context.SaveChangesAsync(cancellationToken);

        //if (command.SendCommunication)
        //{
        //    await Mediator.Send(new ProjectStoreSendCommunicationCommand { ProjectId = command.Id }, cancellationToken);
        //}

        if (comment != null && comment.ProjectCommentId > 0)
        {
            opstatus.ReturnObject = comment.ProjectCommentId;
        }

        opstatus.Status = true;
        opstatus.OperationId = command.Id;

        return opstatus;
    }
}

public class UpdateProjectStoreValidator : AbstractValidator<ProjectStoreUpdateCommand>
{
    public UpdateProjectStoreValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Description).MaximumLength(100);
        RuleFor(e => e.ProjectStoreId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.Note);
        RuleFor(e => e.ProjectTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectPriorityId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.EngageRegionId);
        //RuleFor(e => e.ProjectCampaignId);
        //RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}