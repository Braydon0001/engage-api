using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskUpdateCommand : IMapTo<ProjectTask>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Note { get; init; }
    public int ProjectId { get; init; }
    public int ProjectTaskStatusId { get; set; }
    public int? ProjectTaskTypeId { get; set; }
    //public int? ProjectTaskSeverityId { get; set; }
    public int ProjectTaskPriorityId { get; set; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public float? EstimatedHours { get; init; }
    public float? RemainingHours { get; init; }
    public int? UserId { get; set; }
    public int? ProjectStakeholderId { get; set; }
    public List<int> ProjectStakeholderIds { get; set; }
    public bool IsClosed { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskUpdateCommand, ProjectTask>();
    }
}

public record ProjectTaskUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options) : IRequestHandler<ProjectTaskUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectTaskUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTasks.SingleOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var taskClosed = command.IsClosed && !entity.IsClosed;

        var mappedEntity = Mapper.Map(command, entity);
        mappedEntity.Note = null;
        if (command.ProjectStakeholderIds != null && command.ProjectStakeholderIds.Count > 0)
        {
            if (command.ProjectTaskStatusId < (int)ProjectTaskStatusId.Completed)
            {
                mappedEntity.ProjectTaskStatusId = (int)ProjectTaskStatusId.Assigned;
            }
        }
        if (command.IsClosed && command.ProjectTaskStatusId != (int)ProjectTaskStatusId.Completed)
        {
            mappedEntity.ProjectTaskStatusId = (int)ProjectTaskStatusId.Completed;
        }

        var existingStakeholders = await Context.ProjectTaskProjectStakeholderUsers.Where(e => e.ProjectTaskId == command.Id)
                                                                                       .ToListAsync(cancellationToken);
        List<int> newStakeholders = [];
        List<int> removeStakeholders = [];

        if (command.ProjectStakeholderIds != null && command.ProjectStakeholderIds.Count > 0)
        {
            newStakeholders = command.ProjectStakeholderIds.Except(existingStakeholders.Select(e => e.ProjectStakeholderId)).ToList();
            removeStakeholders = existingStakeholders.Select(e => e.ProjectStakeholderId).Except(command.ProjectStakeholderIds).ToList();

            if (newStakeholders.Any())
            {
                foreach (var stakeholderId in newStakeholders)
                {
                    var newStakeholder = new ProjectTaskProjectStakeholderUser
                    {
                        ProjectTaskId = command.Id,
                        ProjectStakeholderId = stakeholderId
                    };
                    Context.ProjectTaskProjectStakeholderUsers.Add(newStakeholder);
                }
            }

            if (removeStakeholders.Any())
            {
                foreach (var stakeholderId in removeStakeholders)
                {
                    var removeStakeholder = existingStakeholders.Where(e => e.ProjectStakeholderId == stakeholderId).FirstOrDefault();
                    if (removeStakeholder != null)
                    {
                        Context.ProjectTaskProjectStakeholderUsers.Remove(removeStakeholder);
                    }
                }
            }
        }
        else
        {
            if (existingStakeholders.Count > 0)
            {
                Context.ProjectTaskProjectStakeholderUsers.RemoveRange(existingStakeholders);
            }
        }

        if (!string.IsNullOrEmpty(command.Note))
        {
            Context.ProjectTaskNotes.Add(new ProjectTaskNote
            {
                ProjectTask = mappedEntity,
                Note = command.Note,
            });
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        var sendEmail = newStakeholders.Count > 0 || removeStakeholders.Count > 0;

        if (opStatus.Status && sendEmail || opStatus.Status && taskClosed)
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
                        if (assignedUsers.Count == 0)
                        {
                            defaultTo = tacOps.Select(e => e).First();
                        }
                        tacOps.Remove(defaultTo);
                        if (tacOps.Count > 0)
                        {
                            ccEmails.AddRange(tacOps);
                        }
                    }
                }
            }

            if (projectTemplate != null)
            {
                if (ccEmails.Count > 0)
                {
                    //ccEmails.RemoveIfContains(defaultTo);
                    if (command.ProjectStakeholderIds != null && command.ProjectStakeholderIds.Count > 0)
                    {
                        ccEmails.RemoveRangeIfContains(assignedUsers.Select(e => e.User.Email).ToArray());
                        //ccEmails.RemoveIfContains(assignedUser.User.Email);
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
                        assignedName = assignedUsers.Count > 0 ? string.Join(", ", assignedUsers.Select(e => $"{e.User.FullName} - {e.User.Email}")) : string.Empty,
                        StatusName = task.ProjectTaskStatus.Name,
                        //SeverityName = task.ProjectTaskSeverity.Name,
                        PriorityName = task.ProjectTaskPriority.Name,
                        UserEmail = entity.CreatedBy
                    }
                }, cancellationToken);
            }
        }

        opStatus.OperationId = entity.ProjectTaskId;
        return opStatus;
    }
}

public class UpdateProjectTaskValidator : AbstractValidator<ProjectTaskUpdateCommand>
{
    public UpdateProjectTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        //RuleFor(e => e.Note).MaximumLength(1000);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskStatusId).NotEmpty().GreaterThan(0);
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