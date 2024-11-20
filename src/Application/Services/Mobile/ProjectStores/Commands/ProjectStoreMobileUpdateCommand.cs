using Engage.Application.Services.ProjectProjectTags.Commands;
using Engage.Application.Services.ProjectStores.Commands;

namespace Engage.Application.Services.Mobile.ProjectStores;

public class ProjectStoreMobileUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int UserId { get; init; }
    public string Name { get; init; }
    public int StoreId { get; init; }
    public int ProjectTypeId { get; init; }
    public int ProjectPriorityId { get; init; }
    public int? ProjectCampaignId { get; init; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; init; }
    //public List<FileUpload> Files { get; init; }
    //public List<ProjectStoreMobileNote> Notes { get; init; }
    public ProjectStoreMobileTags Tags { get; init; }
}

public record ProjectStoreMobileUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options, IFileService File) : IRequestHandler<ProjectStoreMobileUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreMobileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectStores.SingleOrDefaultAsync(e => e.ProjectId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        //update the project
        var projectStoreUpdateCommand = new ProjectStoreUpdateCommand
        {
            Id = command.Id,
            Description = command.Name,
            ProjectStoreId = command.StoreId,
            ProjectTypeId = command.ProjectTypeId,
            ProjectPriorityId = command.ProjectPriorityId,
            //ProjectCampaignId = command.ProjectCampaignId,
            //StartDate = command.StartDate,
            EndDate = command.EndDate,
        };

        var mappedEntity = Mapper.Map(projectStoreUpdateCommand, entity);

        entity.ProjectStatusId = (int)ProjectStatusId.Unassigned;

        //get the current project tags
        var currentTags = await Context.ProjectProjectTags.Where(e => e.ProjectId == entity.ProjectId)
                                                          .ToListAsync(cancellationToken);

        //get the different tag types
        var currentClaims = currentTags.OfType<ProjectProjectTagClaim>().ToList();
        var currentDCProducts = currentTags.OfType<ProjectProjectTagDCProduct>().ToList();
        var currentEmployeeJobTitles = currentTags.OfType<ProjectProjectTagEmployeeJobTitle>().ToList();
        var currentEngageRegions = currentTags.OfType<ProjectProjectTagEngageRegion>().ToList();
        var currentStores = currentTags.OfType<ProjectProjectTagStore>().ToList();
        var currentSuppliers = currentTags.OfType<ProjectProjectTagSupplier>().ToList();
        var currentUsers = currentTags.OfType<ProjectProjectTagUser>().ToList();

        //get the tags to delete
        var claimsToDelete = currentClaims.Where(e => !command.Tags.ClaimIds.Contains(e.ClaimId)).ToList();
        var dcProductsToDelete = currentDCProducts.Where(e => !command.Tags.DCProductIds.Contains(e.DCProductId)).ToList();
        var employeeJobTitlesToDelete = currentEmployeeJobTitles.Where(e => !command.Tags.EmployeeJobTitleIds.Contains(e.EmployeeJobTitleId)).ToList();
        var engageRegionsToDelete = currentEngageRegions.Where(e => !command.Tags.EngageRegionIds.Contains(e.EngageRegionId)).ToList();
        var storesToDelete = currentStores.Where(e => !command.Tags.StoreIds.Contains(e.StoreId)).ToList();
        var suppliersToDelete = currentSuppliers.Where(e => !command.Tags.SupplierIds.Contains(e.SupplierId)).ToList();
        var usersToDelete = currentUsers.Where(e => !command.Tags.UserIds.Contains(e.UserId)).ToList();

        //get all the projectTagIds to delete
        var projectTagIdsToDelete = claimsToDelete.Select(e => e.ProjectProjectTagId)
                                                 .Union(dcProductsToDelete.Select(e => e.ProjectProjectTagId))
                                                 .Union(employeeJobTitlesToDelete.Select(e => e.ProjectProjectTagId))
                                                 .Union(engageRegionsToDelete.Select(e => e.ProjectProjectTagId))
                                                 .Union(storesToDelete.Select(e => e.ProjectProjectTagId))
                                                 .Union(suppliersToDelete.Select(e => e.ProjectProjectTagId))
                                                 .Union(usersToDelete.Select(e => e.ProjectProjectTagId))
                                                 .ToList();

        //delete the tags
        if (projectTagIdsToDelete.Count > 0)
        {
            var currentTagsToDelete = currentTags.Where(e => projectTagIdsToDelete.Contains(e.ProjectProjectTagId)).ToList();
            Context.ProjectProjectTags.RemoveRange(currentTagsToDelete);
        }

        //add the tags
        var tagCommand = new ProjectProjectTagInsertCommand
        {
            ProjectId = entity.ProjectId,
            ClaimIds = command.Tags.ClaimIds,
            DCProductIds = command.Tags.DCProductIds,
            EmployeeJobTitleIds = command.Tags.EmployeeJobTitleIds,
            EngageRegionIds = command.Tags.EngageRegionIds,
            StoreIds = command.Tags.StoreIds,
            SupplierIds = command.Tags.SupplierIds,
            UserIds = command.Tags.UserIds,
            StoreAssetIds = command.Tags.StoreAssetIds
        };

        await Mediator.Send(tagCommand, cancellationToken);

        ////get the files currently associated with the project
        //var existingProjectFiles = await Context.ProjectFiles.Where(e => e.ProjectId == entity.ProjectId)
        //                                                     .ToListAsync(cancellationToken);

        ////if we have files but the command does not, delete the files
        //if (existingProjectFiles != null && (command.Files == null || command.Files.Count == 0))
        //{
        //    foreach (var file in existingProjectFiles)
        //    {
        //        var fileCommand = new ProjectFileDeleteCommand
        //        {
        //            Id = file.ProjectFileId
        //        };

        //        var fileResult = await Mediator.Send(fileCommand, cancellationToken);
        //    }
        //}

        ////Add the files
        //if (command.Files != null && command.Files.Count > 0)
        //{
        //    var ProjectFileType = await Context.ProjectFileTypes.ToListAsync(cancellationToken: cancellationToken);

        //    //find the difference between what was submitted and what already exists
        //    //var filesToKeepIds = command.Files.Select(e => e.Id).Intersect(existingFiles).ToList();
        //    var filesToDeleteIds = existingProjectFiles.Where(e => e.Files.Any).Select(e => e.ProjectFileId).ToList();
        //    //delete the files that are not in the list
        //    foreach (var file in filesToDeleteIds)
        //    {
        //        var fileCommand = new ProjectFileDeleteCommand
        //        {
        //            Id = file
        //        };

        //        var fileResult = await Mediator.Send(fileCommand, cancellationToken);
        //    }

        //    //add the new files
        //    foreach (var uploadedFile in command.Files.Where(e => e.Id == 0))
        //    {
        //        var ProjectFileTypeId = ProjectFileType.SingleOrDefault(e => e.Name.Equals(uploadedFile.FileType, StringComparison.CurrentCultureIgnoreCase)).ProjectFileTypeId;
        //        var fileCommand = new ProjectFileInsertCommand
        //        {
        //            ProjectId = entity.ProjectId,
        //            ProjectFileTypeId = ProjectFileTypeId
        //        };

        //        var file = await Mediator.Send(fileCommand, cancellationToken);

        //        var fileUploadCommand = new ProjectFileFileUploadCommand
        //        {
        //            File = uploadedFile.File,
        //            FileType = uploadedFile.FileType,
        //            Id = file.ProjectFileId,
        //            ProjectId = entity.ProjectId
        //        };

        //        var fileResult = await Mediator.Send(fileUploadCommand, cancellationToken);
        //    }
        //}

        ////add the notes
        //if (command.Notes != null)
        //{
        //    //get the existing notes
        //    var existingNotes = await Context.ProjectNotes.Where(e => e.ProjectId == entity.ProjectId)
        //                                                  .Select(e => e.ProjectNoteId)
        //                                                  .ToListAsync(cancellationToken);

        //    //find the difference between what was submitted and what already exists
        //    var notesToKeepIds = command.Notes.Select(e => e.Id).Intersect(existingNotes).ToList();
        //    var notesToDeleteIds = existingNotes.Except(notesToKeepIds).ToList();

        //    //delete the notes that are not in the list
        //    foreach (var note in notesToDeleteIds)
        //    {
        //        var noteDeleteCommand = new ProjectNoteDeleteCommand(note);

        //        var noteResult = await Mediator.Send(noteDeleteCommand, cancellationToken);
        //    }

        //    //update the notes that are being kept
        //    foreach (var note in command.Notes.Where(e => e.Id > 0))
        //    {
        //        var noteUpdateCommand = new ProjectNoteUpdateCommand
        //        {
        //            Id = note.Id,
        //            Note = note.Note
        //        };

        //        var noteResult = await Mediator.Send(noteUpdateCommand, cancellationToken);

        //        //update their files
        //        if (note.Files != null)
        //        {
        //            foreach (var uploadedFile in note.Files)
        //            {
        //                var fileUploadCommand = new ProjectNoteFileUploadCommand
        //                {
        //                    File = uploadedFile.File,
        //                    FileType = uploadedFile.FileType,
        //                    Id = uploadedFile.Id,
        //                };

        //                var fileResult = await Mediator.Send(fileUploadCommand, cancellationToken);

        //            }
        //        }
        //    }

        //    //add the new notes
        //    foreach (var note in command.Notes.Where(e => e.Id == 0))
        //    {
        //        var projectNote = new ProjectNote
        //        {
        //            ProjectId = entity.ProjectId,
        //            Note = note.Note,
        //        };

        //        Context.ProjectNotes.Add(projectNote);

        //        if (note.Files != null)
        //        {
        //            foreach (var uploadedFile in note.Files)
        //            {

        //                var fileUploadCommand = new ProjectNoteFileUploadCommand
        //                {
        //                    File = uploadedFile.File,
        //                    FileType = uploadedFile.FileType,
        //                    Id = uploadedFile.Id,
        //                };

        //                var fileResult = await Mediator.Send(fileUploadCommand, cancellationToken);

        //            }
        //        }
        //    }
        //}

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        opStatus.OperationId = entity.ProjectId;

        return opStatus;
    }
}

public class ProjectStoreMobileUpdateValidator : AbstractValidator<ProjectStoreMobileUpdateCommand>
{
    public ProjectStoreMobileUpdateValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectPriorityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectCampaignId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}
