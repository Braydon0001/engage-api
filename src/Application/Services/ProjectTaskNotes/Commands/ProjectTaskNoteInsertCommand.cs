using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.ProjectTaskNotes.Commands;

public class ProjectTaskNoteInsertCommand : IMapTo<ProjectTaskNote>, IRequest<OperationStatus>
{
    public string Note { get; init; }
    public int ProjectTaskId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskNoteInsertCommand, ProjectTaskNote>();
    }
}

public record ProjectTaskNoteInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options) : IRequestHandler<ProjectTaskNoteInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectTaskNoteInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskNoteInsertCommand, ProjectTaskNote>(command);

        Context.ProjectTaskNotes.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status)
        {
            var task = await Context.ProjectTasks.Include(e => e.Project)
                                                 .Include(e => e.ProjectStakeholder)
                                                    .ThenInclude(e => e.User)
                                                 .Where(e => e.ProjectTaskId == command.ProjectTaskId)
                                                 .FirstOrDefaultAsync(cancellationToken);

            var defaultTo = Options.Value.DefaultToEmail;
            List<string> ccEmails = new List<string>();
            var projectTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.NewTaskComment)
                                                                      .FirstOrDefaultAsync(cancellationToken);

            var store = await Context.ProjectStores.Include(e => e.Store)
                                                   .Where(e => e.ProjectId == task.ProjectId)
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

            if (projectTemplate != null)
            {
                await Mediator.Send(new SendEmailCommand
                {
                    ToEmailAddress = task.ProjectStakeholderId.HasValue ? task.ProjectStakeholder.User.Email : defaultTo,
                    FromEmailAddress = projectTemplate.FromEmailAddress,
                    FromEmailName = projectTemplate.FromName,
                    Subject = projectTemplate.Subject,
                    Body = projectTemplate.Body,
                    CcEmailAddresses = ccEmails,
                    TemplateData = new
                    {
                        ProjectName = task.Project.Name,
                        TaskName = task.Name,
                        AssignedName = task.ProjectStakeholder != null ? $"{task.ProjectStakeholder.User.FullName} - {task.ProjectStakeholder.User.Email}" : string.Empty,
                        Comment = command.Note,
                        UserEmail = entity.CreatedBy
                    }
                }, cancellationToken);
            }

            #region Uncomment below if we're using Front-End Email Service
            //if (task.ProjectStakeholderId.HasValue)
            //{
            //    var toEmail = string.Empty;
            //    var assignedTo = string.Empty;

            //    var regionContact = await Context.ProjectStakeholderEmployeeRegionContacts.Include(e => e.EmployeeRegionContact)
            //                                                                              .ThenInclude(e => e.Employee)
            //                                                                              .FirstOrDefaultAsync(e => e.ProjectStakeholderId == task.ProjectStakeholderId.Value, cancellationToken);

            //    var storeContact = await Context.ProjectStakeholderStoreContacts.Include(e => e.StoreContact)
            //                                                                    .FirstOrDefaultAsync(e => e.ProjectStakeholderId == task.ProjectStakeholderId.Value, cancellationToken);

            //    var supplierContact = await Context.ProjectStakeholderSupplierContacts.Include(e => e.SupplierContact)
            //                                                                          .FirstOrDefaultAsync(e => e.ProjectStakeholderId == task.ProjectStakeholderId.Value, cancellationToken);

            //    var user = await Context.ProjectStakeholderUsers.Include(e => e.User)
            //                                                    .FirstOrDefaultAsync(e => e.ProjectStakeholderId == task.ProjectStakeholderId.Value, cancellationToken);

            //    if (regionContact != null)
            //    {
            //        toEmail = regionContact.EmployeeRegionContact.Employee.EmailAddress1;
            //        assignedTo = regionContact.EmployeeRegionContact.Employee.FirstName + " " + regionContact.EmployeeRegionContact.Employee.LastName + " - " + regionContact.EmployeeRegionContact.Employee.EmailAddress1 + " (Region Contact)";
            //    }

            //    if (storeContact != null)
            //    {
            //        toEmail = storeContact.StoreContact.EmailAddress1;
            //        assignedTo = storeContact.StoreContact.FirstName + " " + storeContact.StoreContact.LastName + " - " + storeContact.StoreContact.EmailAddress1 + " (Store Contact)";
            //    }

            //    if (supplierContact != null)
            //    {
            //        toEmail = supplierContact.SupplierContact.EmailAddress1;
            //        assignedTo = supplierContact.SupplierContact.FirstName + " " + supplierContact.SupplierContact.LastName + " - " + supplierContact.SupplierContact.EmailAddress1 + " (Supplier Contact)";
            //    }

            //    if (user != null)
            //    {
            //        toEmail = user.User.Email;
            //        assignedTo = user.User.FirstName + " " + user.User.LastName + " - " + user.User.Email + " (User)";
            //    }

            //    var noteTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.NewTaskComment)
            //                                                           .FirstOrDefaultAsync(cancellationToken);

            //    var template = Mapper.Map<ProjectEmailVm>(noteTemplate);

            //    if (noteTemplate != null)
            //    {
            //        template.AssignedTo = assignedTo;
            //        template.ProjectName = task.Project.Name;
            //        template.TaskName = task.Name;
            //        template.CreatedBy = entity.CreatedBy;
            //        template.ToEmailAddress = toEmail;
            //    }

            //    if (template.ToEmailAddress != null)
            //    {
            //        opStatus.ReturnObject = template;
            //    }
            //}
            #endregion
        }

        opStatus.OperationId = entity.ProjectTaskId;
        return opStatus;
    }
}

public class ProjectTaskNoteInsertValidator : AbstractValidator<ProjectTaskNoteInsertCommand>
{
    public ProjectTaskNoteInsertValidator()
    {
        RuleFor(e => e.Note).NotEmpty().MaximumLength(1000);
        RuleFor(e => e.ProjectTaskId);
    }
}