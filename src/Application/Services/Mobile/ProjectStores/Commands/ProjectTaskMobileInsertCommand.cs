using Engage.Application.Services.ProjectTasks.Commands;

namespace Engage.Application.Services.Mobile.ProjectStores.Commands;

public class ProjectTaskMobileInsertCommand : IMapTo<ProjectTask>, IRequest<OperationStatus>
{
    public string Name { get; init; }
    public int ProjectId { get; init; }
    public int? ProjectTaskStatusId { get; init; }
    public int? ProjectTaskTypeId { get; init; }
    public int? ProjectTaskPriorityId { get; set; }
    public int? ProjectTaskSeverityId { get; set; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<int> ProjectStakeholderIds { get; set; }
    public List<FileUpload> Files { get; init; }
    //public List<ProjectStoreMobileNote> Notes { get; init; }
}

public record ProjectTaskMobileInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectTaskMobileInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectTaskMobileInsertCommand command, CancellationToken cancellationToken)
    {
        var projectTaskInsertCommand = new ProjectTaskInsertCommand
        {
            Name = command.Name,
            ProjectId = command.ProjectId,
            ProjectTaskStatusId = command.ProjectTaskStatusId,
            ProjectTaskTypeId = command.ProjectTaskTypeId,
            ProjectTaskPriorityId = command.ProjectTaskPriorityId,
            //ProjectTaskSeverityId = command.ProjectTaskSeverityId,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            EstimatedHours = null,
            RemainingHours = null,
            UserId = null,
            ProjectStakeholderIds = command.ProjectStakeholderIds
        };

        //var entity = Mapper.Map<ProjectTaskInsertCommand, ProjectTask>(projectTaskInsertCommand);
        var entity = await Mediator.Send(projectTaskInsertCommand);

        //entity.ProjectStakeholderId = command.ProjectStakeholderId;

        //Context.ProjectTasks.Add(entity);

        //add the files
        if (command.Files != null && command.Files.Count > 0)
        {
            foreach (var file in command.Files)
            {
                var fileUploadCommand = new ProjectTaskFileUploadCommand
                {
                    File = file.File,
                    FileType = file.FileType,
                    Id = (int)entity.OperationId,
                };

                await Mediator.Send(fileUploadCommand, cancellationToken);
            }

        }

        ////add the notes
        //if (command.Notes != null && command.Notes.Count > 0)
        //{
        //    foreach (var note in command.Notes)
        //    {
        //        var newNote = new ProjectTaskNote
        //        {
        //            Note = note.Note,
        //            ProjectTaskId = entity.ProjectTaskId
        //        };

        //        var noteEntity = Context.ProjectTaskNotes.Add(newNote);

        //        //note files
        //        if (note.Files != null && note.Files.Count > 0)
        //        {
        //            foreach (var file in note.Files)
        //            {
        //                var fileUploadCommand = new ProjectTaskNoteFileUploadCommand
        //                {
        //                    File = file.File,
        //                    FileType = file.FileType,
        //                    Id = noteEntity.Entity.ProjectTaskNoteId,
        //                };

        //                var fileEntity = await Mediator.Send(fileUploadCommand, cancellationToken);
        //            }
        //        }
        //    }
        //}



        //if (opStatus.Status)
        //{
        //    if (command.ProjectStakeholderId.HasValue)
        //    {
        //        var toEmail = string.Empty;

        //        var regionContact = await Context.ProjectStakeholderEmployeeRegionContacts.Include(e => e.EmployeeRegionContact)
        //                                                                                  .ThenInclude(e => e.Employee)
        //                                                                                  .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //        var storeContact = await Context.ProjectStakeholderStoreContacts.Include(e => e.StoreContact)
        //                                                                        .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //        var supplierContact = await Context.ProjectStakeholderSupplierContacts.Include(e => e.SupplierContact)
        //                                                                              .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //        var user = await Context.ProjectStakeholderUsers.Include(e => e.User)
        //                                                        .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //        if (regionContact != null)
        //        {
        //            toEmail = regionContact.EmployeeRegionContact.Employee.EmailAddress1;
        //        }

        //        if (storeContact != null)
        //        {
        //            toEmail = storeContact.StoreContact.EmailAddress1;
        //        }

        //        if (supplierContact != null)
        //        {
        //            toEmail = supplierContact.SupplierContact.EmailAddress1;
        //        }

        //        if (user != null)
        //        {
        //            toEmail = user.User.Email;
        //        }

        //        var taskTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.TaskUpdated)
        //                                                               .FirstOrDefaultAsync(cancellationToken);

        //        var template = Mapper.Map<ProjectEmailVm>(taskTemplate);

        //        if (taskTemplate != null)
        //        {
        //            var project = await Context.Projects.Where(e => e.ProjectId == command.ProjectId)
        //                                                .FirstOrDefaultAsync(cancellationToken);

        //            if (project != null)
        //            {
        //                template.ProjectName = project.Name;

        //                if (project.EngageRegionId.HasValue)
        //                {
        //                    var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == project.EngageRegionId.Value)
        //                                                                     .Select(e => e.ProjectTacOpId)
        //                                                                     .ToListAsync(cancellationToken);

        //                    if (tacOpsIds != null && tacOpsIds.Count > 0)
        //                    {
        //                        var tacOps = await Context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
        //                                                                .Select(e => e.User.Email)
        //                                                                .Distinct()
        //                                                                .ToListAsync(cancellationToken);

        //                        if (tacOps != null && tacOps.Count > 0)
        //                        {
        //                            tacOps.Remove(toEmail);

        //                            if (tacOps.Count > 0)
        //                            {
        //                                if (template.CcEmails == null)
        //                                {
        //                                    template.CcEmails = tacOps;
        //                                }
        //                                else
        //                                {
        //                                    template.CcEmails.AddRange(tacOps);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            var storeProject = await Context.ProjectStores.Include(e => e.Store)
        //                                                          .Where(e => e.ProjectId == command.ProjectId)
        //                                                          .FirstOrDefaultAsync(cancellationToken);

        //            if (storeProject != null)
        //            {
        //                template.ProjectName = storeProject.Name;

        //                var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == storeProject.Store.EngageRegionId)
        //                                                                 .Select(e => e.ProjectTacOpId)
        //                                                                 .ToListAsync(cancellationToken);

        //                if (tacOpsIds != null && tacOpsIds.Count > 0)
        //                {
        //                    var tacOps = await Context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
        //                                                            .Select(e => e.User.Email)
        //                                                            .Distinct()
        //                                                            .ToListAsync(cancellationToken);

        //                    if (tacOps != null && tacOps.Count > 0)
        //                    {
        //                        tacOps.Remove(toEmail);

        //                        if (tacOps.Count > 0)
        //                        {
        //                            if (template.CcEmails == null)
        //                            {
        //                                template.CcEmails = tacOps;
        //                            }
        //                            else
        //                            {
        //                                template.CcEmails.AddRange(tacOps);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //        }

        //        template.CreatedBy = entity.CreatedBy;
        //        template.ToEmailAddress = toEmail;

        //        if (template.ToEmailAddress != null)
        //        {
        //            opStatus.ReturnObject = template;
        //        }
        //    }
        //}

        //opStatus.OperationId = entity.OperationId;
        return entity;
    }
}

public class ProjectTaskMobileInsertValidator : AbstractValidator<ProjectTaskMobileInsertCommand>
{
    public ProjectTaskMobileInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskTypeId);
        RuleFor(e => e.ProjectTaskPriorityId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        //RuleFor(e => e.ProjectStakeholderId);
    }
}