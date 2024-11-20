using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskInsertCommand : IMapTo<ProjectTask>, IRequest<OperationStatus>
{
    public string Name { get; init; }
    public string Note { get; init; }
    public int ProjectId { get; init; }
    public int? ProjectTaskStatusId { get; init; }
    public int? ProjectTaskTypeId { get; init; }
    public int? ProjectTaskPriorityId { get; set; }
    //public int? ProjectTaskSeverityId { get; set; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public float? EstimatedHours { get; init; } = 0;
    public float? RemainingHours { get; init; } = 0;
    public int? UserId { get; set; }
    public int? ProjectStakeholderId { get; set; }
    public List<int> ProjectStakeholderIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskInsertCommand, ProjectTask>();
    }
}

public record ProjectTaskInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options) : IRequestHandler<ProjectTaskInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectTaskInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskInsertCommand, ProjectTask>(command);
        entity.Note = null;

        if (command.ProjectStakeholderIds != null && command.ProjectStakeholderIds.Count > 0)
        {
            if (command.ProjectTaskStatusId < (int)ProjectTaskStatusId.Completed)
            {
                entity.ProjectTaskStatusId = (int)ProjectTaskStatusId.Assigned;
            }
        }
        else
        {
            entity.ProjectTaskStatusId = (int)ProjectTaskStatusId.Open;
        }

        Context.ProjectTasks.Add(entity);

        if (command.ProjectStakeholderIds != null && command.ProjectStakeholderIds.Count > 0)
        {
            foreach (var stakeholderId in command.ProjectStakeholderIds)
            {
                Context.ProjectTaskProjectStakeholderUsers.Add(new ProjectTaskProjectStakeholderUser
                {
                    ProjectTask = entity,
                    ProjectStakeholderId = stakeholderId
                });
            }
        }

        if (!string.IsNullOrEmpty(command.Note))
        {
            Context.ProjectTaskNotes.Add(new ProjectTaskNote
            {
                ProjectTask = entity,
                Note = command.Note,
            });
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status && command.ProjectStakeholderIds.NotNullOrEmpty())
        {
            var defaultTo = Options.Value.DefaultToEmail;
            List<string> ccEmails = new List<string>();
            var projectTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.TaskUpdated)
                                                                      .FirstOrDefaultAsync(cancellationToken);

            var task = await Context.ProjectTasks.Include(e => e.Project)
                                                 .Include(e => e.ProjectTaskPriority)
                                                 .Include(e => e.ProjectTaskStatus)
                                                 //.Include(e => e.ProjectTaskSeverity)
                                                 .Where(e => e.ProjectTaskId == entity.ProjectTaskId)
                                                 .FirstOrDefaultAsync(cancellationToken);

            var assignedUsers = new List<ProjectStakeholderUser>();
            if (command.ProjectStakeholderIds != null && command.ProjectStakeholderIds.Count > 0)
            {
                assignedUsers = await Context.ProjectStakeholderUsers.Include(e => e.User)
                                                                     .Where(e => command.ProjectStakeholderIds.Contains(e.ProjectStakeholderId))
                                                                     .ToListAsync(cancellationToken);
            }

            var store = await Context.ProjectStores.Include(e => e.Store)
                                                   .Where(e => e.ProjectId == command.ProjectId)
                                                   .FirstOrDefaultAsync(cancellationToken);

            if (store != null)
            {
                var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == store.Store.EngageRegionId)
                                                                 .Select(e => e.ProjectTacOpId)
                                                                 .ToListAsync(cancellationToken);
                if (tacOpsIds != null && tacOpsIds.Count > 0)
                {
                    var tacOps = await Context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
                                                            .Select(e => e.User.Email)
                                                            .Distinct()
                                                            .ToListAsync(cancellationToken);

                    if (tacOps != null && tacOps.Count > 0)
                    {
                        defaultTo = tacOps.Select(e => e).First();
                        tacOps.Remove(defaultTo);
                        if (tacOps.Count > 0)
                        {
                            ccEmails.AddRange(tacOps);
                        }
                    }
                }
            }

            ccEmails.AddRange(assignedUsers.Select(e => e.User.Email));
            ccEmails = ccEmails.Distinct().ToList();
            if (assignedUsers.Count > 0)
            {
                ccEmails.RemoveIfContains(assignedUsers.First().User.Email);
            }
            else
            {
                ccEmails.RemoveIfContains(defaultTo);
            }

            if (projectTemplate != null)
            {
                await Mediator.Send(new SendEmailCommand
                {
                    ToEmailAddress = command.ProjectStakeholderIds != null && command.ProjectStakeholderIds.Count > 0 ? assignedUsers.First().User.Email : defaultTo,
                    FromEmailAddress = projectTemplate.FromEmailAddress,
                    FromEmailName = projectTemplate.FromName,
                    Subject = projectTemplate.Subject,
                    Body = projectTemplate.Body,
                    CcEmailAddresses = ccEmails,
                    TemplateData = new
                    {
                        ProjectName = task.Project.Name,
                        TaskName = task.Name,
                        //AssignedName = command.ProjectStakeholderId.HasValue ? $"{assignedUser.User.FullName} - {assignedUser.User.Email}" : string.Empty,
                        assignedName = assignedUsers.Count > 0 ? string.Join(", ", assignedUsers.Select(e => $"{e.User.FullName} - {e.User.Email}")) : string.Empty,
                        StatusName = task.ProjectTaskStatus.Name,
                        //SeverityName = task.ProjectTaskSeverity.Name,
                        PriorityName = task.ProjectTaskPriority.Name,
                        UserEmail = entity.CreatedBy
                    }
                }, cancellationToken);
            }

            #region Uncomment below if we're using Front-End Email Service
            //var toEmail = string.Empty;

            ////var regionContact = await Context.ProjectStakeholderEmployeeRegionContacts.Include(e => e.EmployeeRegionContact)
            ////                                                                          .ThenInclude(e => e.Employee)
            ////                                                                          .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

            ////var storeContact = await Context.ProjectStakeholderStoreContacts.Include(e => e.StoreContact)
            ////                                                                .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

            ////var supplierContact = await Context.ProjectStakeholderSupplierContacts.Include(e => e.SupplierContact)
            ////                                                                      .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

            //var user = await Context.ProjectStakeholderUsers.Include(e => e.User)
            //                                                .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

            ////if (regionContact != null)
            ////{
            ////    toEmail = regionContact.EmployeeRegionContact.Employee.EmailAddress1;
            ////}

            ////if (storeContact != null)
            ////{
            ////    toEmail = storeContact.StoreContact.EmailAddress1;
            ////}

            ////if (supplierContact != null)
            ////{
            ////    toEmail = supplierContact.SupplierContact.EmailAddress1;
            ////}

            //if (user != null)
            //{
            //    toEmail = user.User.Email;
            //}

            //var taskTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.TaskUpdated)
            //                                                       .FirstOrDefaultAsync(cancellationToken);

            //var template = Mapper.Map<ProjectEmailVm>(taskTemplate);

            //if (taskTemplate != null)
            //{
            //    var project = await Context.Projects.Where(e => e.ProjectId == command.ProjectId)
            //                                        .FirstOrDefaultAsync(cancellationToken);

            //    if (project != null)
            //    {
            //        template.ProjectName = project.Name;

            //        if (project.EngageRegionId.HasValue)
            //        {
            //            var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == project.EngageRegionId.Value)
            //                                                             .Select(e => e.ProjectTacOpId)
            //                                                             .ToListAsync(cancellationToken);

            //            if (tacOpsIds != null && tacOpsIds.Count > 0)
            //            {
            //                var tacOps = await Context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
            //                                                        .Select(e => e.User.Email)
            //                                                        .Distinct()
            //                                                        .ToListAsync(cancellationToken);

            //                if (tacOps != null && tacOps.Count > 0)
            //                {
            //                    tacOps.Remove(toEmail);

            //                    if (tacOps.Count > 0)
            //                    {
            //                        if (template.CcEmails == null)
            //                        {
            //                            template.CcEmails = tacOps;
            //                        }
            //                        else
            //                        {
            //                            template.CcEmails.AddRange(tacOps);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    var storeProject = await Context.ProjectStores.Include(e => e.Store)
            //                                                  .Where(e => e.ProjectId == command.ProjectId)
            //                                                  .FirstOrDefaultAsync(cancellationToken);

            //    if (storeProject != null)
            //    {
            //        template.ProjectName = storeProject.Name;

            //        var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == storeProject.Store.EngageRegionId)
            //                                                         .Select(e => e.ProjectTacOpId)
            //                                                         .ToListAsync(cancellationToken);

            //        if (tacOpsIds != null && tacOpsIds.Count > 0)
            //        {
            //            var tacOps = await Context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
            //                                                    .Select(e => e.User.Email)
            //                                                    .Distinct()
            //                                                    .ToListAsync(cancellationToken);

            //            if (tacOps != null && tacOps.Count > 0)
            //            {
            //                tacOps.Remove(toEmail);

            //                if (tacOps.Count > 0)
            //                {
            //                    if (template.CcEmails == null)
            //                    {
            //                        template.CcEmails = tacOps;
            //                    }
            //                    else
            //                    {
            //                        template.CcEmails.AddRange(tacOps);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //}

            //template.CreatedBy = entity.CreatedBy;
            //template.ToEmailAddress = toEmail;

            //if (template.ToEmailAddress != null)
            //{
            //    opStatus.ReturnObject = template;
            //}
            #endregion

        }

        opStatus.OperationId = entity.ProjectTaskId;
        return opStatus;
    }
}

public class ProjectTaskInsertValidator : AbstractValidator<ProjectTaskInsertCommand>
{
    public ProjectTaskInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(1000);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskTypeId);
        RuleFor(e => e.ProjectTaskPriorityId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        RuleFor(e => e.EstimatedHours);
        RuleFor(e => e.RemainingHours);
        RuleFor(e => e.UserId);
        //RuleFor(e => e.ProjectStakeholderId);
    }
}