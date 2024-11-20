using Engage.Application.Services.ProjectProjectTags.Commands;
using Engage.Application.Services.ProjectStores.Commands;

namespace Engage.Application.Services.Mobile.ProjectStores;

public class ProjectStoreMobileInsertCommand : IRequest<OperationStatus>
{
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

public record ProjectStoreMobileInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options, IFileService File) : IRequestHandler<ProjectStoreMobileInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreMobileInsertCommand command, CancellationToken cancellationToken)
    {
        //create the project
        var projectStoreInsertCommand = new ProjectStoreInsertCommand
        {
            Description = command.Name,
            ProjectStoreId = command.StoreId,
            ProjectTypeId = command.ProjectTypeId,
            ProjectPriorityId = command.ProjectPriorityId,
            //ProjectCampaignId = command.ProjectCampaignId,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            //EstimatedHours = 0,
            //RemainingHours = 0
        };

        var entity = Mapper.Map<ProjectStoreInsertCommand, ProjectStore>(projectStoreInsertCommand);

        entity.ProjectStatusId = (int)ProjectStatusId.Unassigned;
        Context.ProjectStores.Add(entity);

        var saveProject = await Context.SaveChangesAsync(cancellationToken);

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
            UserIds = command.Tags.UserIds
        };

        await Mediator.Send(tagCommand, cancellationToken);

        if (saveProject.Status)
        {
            //assign stakeholders to the project
            List<int> userIds = [];

            userIds.Add(command.UserId);


            var employeeUserIds = await Context.EmployeeStores.Where(e => e.StoreId == command.StoreId)
                                                              .Select(e => e.Employee.UserId)
                                                              .ToListAsync(cancellationToken);

            if (employeeUserIds != null && employeeUserIds.Count > 0)
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

            var store = await Context.Stores.Where(e => e.StoreId == command.StoreId)
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

        }



        ////Add the files
        //if (command.Files != null)
        //{
        //    var ProjectFileType = await Context.ProjectFileTypes.ToListAsync(cancellationToken: cancellationToken);
        //    foreach (var uploadedFile in command.Files)
        //    {
        //        var fileUploadCommand = new ProjectFileFileUploadCommand
        //        {
        //            File = uploadedFile.File,
        //            FileType = uploadedFile.FileType,
        //            Id = uploadedFile.Id,
        //            ProjectId = entity.ProjectId
        //        };

        //        var fileResult = await Mediator.Send(fileUploadCommand, cancellationToken);
        //    }
        //}

        ////add the notes
        //if (command.Notes != null)
        //{
        //    foreach (var note in command.Notes)
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

        //                //await Context.SaveChangesAsync(cancellationToken);
        //            }
        //        }
        //    }
        //}

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        //if (opStatus.Status)
        //{
        //    var projectTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.NewStoreProject)
        //                                                              .FirstOrDefaultAsync(cancellationToken);

        //    var template = Mapper.Map<ProjectEmailVm>(projectTemplate);

        //    if (projectTemplate != null)
        //    {
        //        var defaultTo = Options.Value.DefaultToEmail;

        //        var store = await Context.Stores.Where(e => e.StoreId == command.StoreId)
        //                                           .FirstOrDefaultAsync(cancellationToken);

        //        template.StoreName = store.Name;

        //        var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == store.EngageRegionId)
        //                                                         .Select(e => e.ProjectTacOpId)
        //                                                         .ToListAsync(cancellationToken);

        //        if (tacOpsIds != null && tacOpsIds.Count > 0)
        //        {
        //            var tacOps = await Context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
        //                                                    .Select(e => e.User.Email)
        //                                                    .Distinct()
        //                                                    .ToListAsync(cancellationToken);

        //            var toEmail = tacOps.Select(e => e).First();

        //            if (tacOps != null && tacOps.Count > 0)
        //            {
        //                tacOps.Remove(toEmail);
        //                tacOps.Remove(defaultTo);

        //                template.ToEmailAddress = toEmail;
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

        //        template.ToEmailAddress = template.ToEmailAddress ?? defaultTo;
        //        template.CreatedBy = entity.CreatedBy;

        //        //do not return the template if the toEmailAddress is null
        //        if (template.ToEmailAddress != null)
        //        {
        //            opStatus.ReturnObject = template;
        //        }
        //    }
        //}

        opStatus.OperationId = entity.ProjectId;

        return opStatus;
    }
}

public class ProjectStoreMobileInsertValidator : AbstractValidator<ProjectStoreMobileInsertCommand>
{
    public ProjectStoreMobileInsertValidator()
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

public class ProjectStoreMobileNote
{
    public int Id { get; set; }
    public string Note { get; set; }
    public List<FileUpload> Files { get; set; }
}

public class ProjectStoreMobileTags
{
    public List<int> ClaimIds { get; set; }
    public List<int> DCProductIds { get; set; }
    public List<int> EmployeeJobTitleIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public List<int> StoreIds { get; set; }
    public List<int> SupplierIds { get; set; }
    public List<int> UserIds { get; set; }
    public List<int> StoreAssetIds { get; set; }
}

public class FileUpload
{
    public int Id { get; set; }
    public IFormFile File { get; set; }
    public string FileType { get; set; }
}
