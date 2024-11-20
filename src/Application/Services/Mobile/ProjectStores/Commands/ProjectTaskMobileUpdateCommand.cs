using Engage.Application.Services.ProjectTasks.Commands;
using FileUpload = Engage.Application.Services.Mobile.ProjectStores.FileUpload;

namespace Engage.Application.Mobile.ProjectStores.Commands;

public class ProjectTaskMobileUpdateCommand : IMapTo<ProjectTask>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public int ProjectId { get; init; }
    public int ProjectTaskStatusId { get; set; }
    public int? ProjectTaskTypeId { get; set; }
    public int? ProjectTaskSeverityId { get; set; }
    public int ProjectTaskPriorityId { get; set; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<int> ProjectStakeholderIds { get; set; }
    public bool IsClosed { get; set; }
    public List<FileUpload> Files { get; init; }
    //public List<ProjectStoreMobileNote> Notes { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskMobileUpdateCommand, ProjectTask>();
    }
}

public record ProjectTaskMobileUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectTaskMobileUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectTaskMobileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTasks.SingleOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var projectTaskUpdateCommand = new ProjectTaskUpdateCommand
        {
            Id = command.Id,
            Name = command.Name,
            ProjectId = command.ProjectId,
            ProjectTaskStatusId = command.ProjectTaskStatusId,
            ProjectTaskTypeId = command.ProjectTaskTypeId,
            //ProjectTaskSeverityId = command.ProjectTaskSeverityId,
            ProjectTaskPriorityId = command.ProjectTaskPriorityId,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            ProjectStakeholderIds = command.ProjectStakeholderIds,
            IsClosed = command.IsClosed
        };

        var updateTask = await Mediator.Send(projectTaskUpdateCommand);


        //mappedEntity.UserId = command.StakeholderId;

        //get the files currently on the task
        var existingFiles = entity.Files;

        //if we had files but the command is null or empty, then we need to remove the files
        if (existingFiles != null && (command.Files == null || command.Files.Count == 0))
        {
            entity.Files = [];
            foreach (var file in existingFiles)
            {
                await Mediator.Send(new ProjectTaskFileDeleteCommand { Id = entity.ProjectTaskId, FileName = file.Name, FileType = file.Type }, cancellationToken);
            }
        }

        //add the files
        if (command.Files != null)
        {
            //get the files to keep
            //var filesToKeep = command.Files.Where(f => existingFiles.Select(e => e.Name).Contains(f.File.Name)).ToList();
            //get the files to delete
            var filesToDeleteNames = existingFiles.Where(e => !command.Files.Select(f => f.File.Name).Contains(e.Name)).ToList();
            //get the new files to add
            var filesToAdd = command.Files.Where(f => !existingFiles.Select(e => e.Name).Contains(f.File.Name)).ToList();

            //delete the files
            foreach (var file in filesToDeleteNames)
            {
                await Mediator.Send(new ProjectTaskFileDeleteCommand { Id = entity.ProjectTaskId, FileName = file.Name, FileType = file.Type }, cancellationToken);
            }

            //add the files
            foreach (var file in filesToAdd)
            {
                await Mediator.Send(new ProjectTaskFileUploadCommand { Id = file.Id, File = file.File, FileType = file.FileType }, cancellationToken);
            }
        }

        ////get the notes currently on the task
        //var existingNotes = await Context.ProjectTaskNotes.Where(e => e.ProjectTaskId == mappedEntity.ProjectTaskId)
        //                                                  .ToListAsync(cancellationToken);

        ////if we had notes but the command is null or empty, then we need to remove the notes
        //if (existingNotes != null && (command.Notes == null || command.Notes.Count == 0))
        //{
        //    foreach (var note in existingNotes)
        //    {
        //        await Mediator.Send(new ProjectTaskNoteDeleteCommand(note.ProjectTaskNoteId), cancellationToken);
        //    }
        //}

        ////add the notes
        //if (command.Notes != null)
        //{
        //    //get the notes to keep
        //    var notesToKeep = command.Notes.Where(f => existingNotes.Select(e => e.Note).Contains(f.Note)).ToList();
        //    //get the notes to delete
        //    var notesToDelete = existingNotes.Where(e => !command.Notes.Select(f => f.Note).Contains(e.Note)).ToList();
        //    //get the new notes to add
        //    var notesToAdd = command.Notes.Where(f => !existingNotes.Select(e => e.Note).Contains(f.Note)).ToList();

        //    //for the notes we are keeping, we must check for changes to the files
        //    foreach (var note in notesToKeep)
        //    {
        //        var existingNote = existingNotes.FirstOrDefault(e => e.Note == note.Note);
        //        if (existingNote != null)
        //        {
        //            //get the files currently on the note
        //            var existingNoteFiles = existingNote.Files;

        //            //if we had files but the command is null or empty, then we need to remove the files
        //            if (existingNoteFiles != null && (note.Files == null || note.Files.Count == 0))
        //            {
        //                existingNote.Files = [];
        //                foreach (var file in existingNoteFiles)
        //                {
        //                    await Mediator.Send(new ProjectTaskNoteFileDeleteCommand { Id = existingNote.ProjectTaskNoteId, FileName = file.Name, FileType = file.Type }, cancellationToken);
        //                }
        //            }

        //            //add the files
        //            if (note.Files != null)
        //            {
        //                //get the files to keep
        //                //var filesToKeep = note.Files.Where(f => existingNoteFiles.Select(e => e.Name).Contains(f.File.Name)).ToList();
        //                //get the files to delete
        //                var filesToDeleteNames = existingNoteFiles.Where(e => !note.Files.Select(f => f.File.Name).Contains(e.Name)).ToList();
        //                //get the new files to add
        //                var filesToAdd = note.Files.Where(f => !existingNoteFiles.Select(e => e.Name).Contains(f.File.Name)).ToList();

        //                //delete the files
        //                foreach (var file in filesToDeleteNames)
        //                {
        //                    await Mediator.Send(new ProjectTaskNoteFileDeleteCommand { Id = existingNote.ProjectTaskNoteId, FileName = file.Name, FileType = file.Type }, cancellationToken);
        //                }

        //                //add the files
        //                foreach (var file in filesToAdd)
        //                {
        //                    await Mediator.Send(new ProjectTaskNoteFileUploadCommand { Id = file.Id, File = file.File, FileType = file.FileType }, cancellationToken);
        //                }
        //            }
        //        }
        //    }

        //    //delete the notes
        //    foreach (var note in notesToDelete)
        //    {
        //        await Mediator.Send(new ProjectTaskNoteDeleteCommand(note.ProjectTaskNoteId), cancellationToken);
        //    }

        //    //add the notes
        //    foreach (var note in notesToAdd)
        //    {
        //        await Mediator.Send(new ProjectTaskNoteInsertCommand { ProjectTaskId = mappedEntity.ProjectTaskId, Note = note.Note }, cancellationToken);
        //        //add the files
        //        if (note.Files != null)
        //        {
        //            foreach (var file in note.Files)
        //            {
        //                await Mediator.Send(new ProjectTaskNoteFileUploadCommand { Id = file.Id, File = file.File, FileType = file.FileType }, cancellationToken);
        //            }
        //        }
        //    }
        //}


        //if (opStatus.Status)
        //{
        //    if (command.ProjectStakeholderId.HasValue)
        //    {
        //        var toEmail = string.Empty;

        //var regionContact = await Context.ProjectStakeholderEmployeeRegionContacts.Include(e => e.EmployeeRegionContact)
        //                                                                          .ThenInclude(e => e.Employee)
        //                                                                          .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //var storeContact = await Context.ProjectStakeholderStoreContacts.Include(e => e.StoreContact)
        //                                                                .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //var supplierContact = await Context.ProjectStakeholderSupplierContacts.Include(e => e.SupplierContact)
        //                                                                      .FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId.Value, cancellationToken);

        //var user = await Context.ProjectStakeholderUsers.Include(e => e.User)
        //                                                .FirstOrDefaultAsync(e => e.ProjectStakeholderId == command.ProjectStakeholderId.Value, cancellationToken);

        //if (regionContact != null)
        //{
        //    toEmail = regionContact.EmployeeRegionContact.Employee.EmailAddress1;
        //}

        //if (storeContact != null)
        //{
        //    toEmail = storeContact.StoreContact.EmailAddress1;
        //}

        //if (supplierContact != null)
        //{
        //    toEmail = supplierContact.SupplierContact.EmailAddress1;
        //}

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

        //        template.CreatedBy = entity.CreatedBy;
        //        template.ToEmailAddress = toEmail;

        //        if (template.ToEmailAddress != null)
        //        {
        //            opStatus.ReturnObject = template;
        //        }
        //    }
        //}

        //opStatus.OperationId = entity.ProjectTaskId;
        return updateTask;
    }
}

public class ProjectTaskMobileUpdateValidator : AbstractValidator<ProjectTaskMobileUpdateCommand>
{
    public ProjectTaskMobileUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskTypeId);
        RuleFor(e => e.ProjectTaskPriorityId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        //RuleFor(e => e.ProjectStakeholderId);
    }
}