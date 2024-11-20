using Engage.Application.Services.CommunicationHistoryProjects.Commands;
using Engage.Application.Services.Emails.Commands;
using Engage.Application.Services.WhatsApp.Commands;

namespace Engage.Application.Services.ProjectComments.Commands;

public class ProjectCommentSendCommunicationCommand : IRequest<OperationStatus>
{
    public int ProjectCommentId { get; init; }
    public bool HasFiles { get; set; }
}

public record ProjectCommentSendCommunicationCommandHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectCommentSendCommunicationCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectCommentSendCommunicationCommand command, CancellationToken cancellationToken)
    {
        var comment = await Context.ProjectComments.SingleOrDefaultAsync(e => e.ProjectCommentId == command.ProjectCommentId, cancellationToken);
        if (comment == null)
        {
            return null;
        }

        var project = await Context.ProjectStores.Include(p => p.Store)
                                                 .Include(p => p.Owner)
                                                 .Include(p => p.ProjectPriority)
                                                 .Include(p => p.ProjectType)
                                                 .Include(p => p.ProjectSubType)
                                                 .Include(p => p.ProjectCategory)
                                                 .Include(p => p.ProjectSubCategory)
                                                 .Include(p => p.ProjectStatus)
                                                 .Include(p => p.ProjectSuppliers)
                                                    .ThenInclude(s => s.Supplier)
                                                 .FirstOrDefaultAsync(e => e.ProjectId == comment.ProjectId, cancellationToken) ?? throw new Exception("Project not found");

        if (project != null)
        {
            var assigned = await Context.ProjectAssignees.AsNoTracking()
                                                         .Where(e => e.ProjectId == project.ProjectId)
                                                         .Include(e => e.ProjectStakeholder)
                                                         .Select(e => e.ProjectStakeholder)
                                                         .ToListAsync(cancellationToken);

            var usersAssigned = assigned.OfType<ProjectStakeholderUser>().Select(e => e.ProjectStakeholderId).ToList();
            var externalUserAssigned = assigned.OfType<ProjectStakeholderExternalUser>().Select(e => e.ProjectStakeholderId).ToList();
            var storeContactAssigned = assigned.OfType<ProjectStakeholderStoreContact>().Select(e => e.ProjectStakeholderId).ToList();
            var supplierContactAssigned = assigned.OfType<ProjectStakeholderSupplierContact>().Select(e => e.ProjectStakeholderId).ToList();
            var regionContactAssigned = assigned.OfType<ProjectStakeholderEmployeeRegionContact>().Select(e => e.ProjectStakeholderId).ToList();

            var userEmails = await Context.ProjectStakeholderUsers.AsNoTracking()
                                                                   .Where(e => usersAssigned.Contains(e.ProjectStakeholderId))
                                                                   .Include(e => e.User)
                                                                   //.Select(e => e.User.Email)
                                                                   .ToListAsync(cancellationToken);

            var externalUserEmails = await Context.ProjectStakeholderExternalUsers.AsNoTracking()
                                                                                   .Where(e => externalUserAssigned.Contains(e.ProjectStakeholderId) && e.ProjectExternalUser.CommunicationTypes.Any(f => f.CommunicationTypeId == (int)CommunicationTypeId.Email))
                                                                                   .Include(e => e.ProjectExternalUser)
                                                                                   //.Select(e => e.ProjectExternalUser.Email)
                                                                                   .ToListAsync(cancellationToken);

            var storeContactEmails = await Context.ProjectStakeholderStoreContacts.Where(e => storeContactAssigned.Contains(e.ProjectStakeholderId))
                                                                                  .Include(e => e.StoreContact)
                                                                                   //.Select(e => e.StoreContact.EmailAddress1)
                                                                                   .ToListAsync(cancellationToken);

            var supplierContactEmails = await Context.ProjectStakeholderSupplierContacts.Where(e => supplierContactAssigned.Contains(e.ProjectStakeholderId))
                                                                                        .Include(e => e.SupplierContact)
                                                                                        //.Select(e => e.SupplierContact.EmailAddress1)
                                                                                        .ToListAsync(cancellationToken);
            var owner = await Context.ProjectStakeholderUsers.Where(e => e.UserId == project.OwnerId && e.ProjectId == project.ProjectId)
                                                         .Include(e => e.User)
                                                         .FirstOrDefaultAsync(cancellationToken);

            List<string> emails = [.. userEmails.Select(e => e.User.Email),
                .. externalUserEmails.Select(e => e.ProjectExternalUser.Email),
                .. storeContactEmails.Select(e => e.StoreContact.EmailAddress1),
                .. supplierContactEmails.Select(e => e.SupplierContact.EmailAddress1)];

            Dictionary<string, int> AssigneeEmailStakeholderIds = new();

            userEmails.ForEach(stakeholder =>
            {
                AssigneeEmailStakeholderIds.Add(stakeholder.User.Email, stakeholder.ProjectStakeholderId);
            });
            externalUserEmails.ForEach(stakeholder =>
            {
                AssigneeEmailStakeholderIds.Add(stakeholder.ProjectExternalUser.Email, stakeholder.ProjectStakeholderId);
            });
            storeContactEmails.ForEach(stakeholder =>
            {
                AssigneeEmailStakeholderIds.Add(stakeholder.StoreContact.EmailAddress1, stakeholder.ProjectStakeholderId);
            });
            supplierContactEmails.ForEach(stakeholder =>
            {
                AssigneeEmailStakeholderIds.Add(stakeholder.SupplierContact.EmailAddress1, stakeholder.ProjectId);
            });

            if (owner != null)
            {
                AssigneeEmailStakeholderIds.AddIfNotContainsKey(owner.User.Email, owner.ProjectStakeholderId);
            }


            //var externalUserNumbers = await Context.ProjectStakeholderExternalUsers.AsNoTracking()
            //                                                                       .Where(e => externalUserAssigned.Contains(e.ProjectStakeholderId) && e.ProjectExternalUser.CommunicationTypes.Any(f => f.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp))
            //                                                                       .Select(e => e.ProjectExternalUser.CellNumber)
            //                                                                       .ToListAsync(cancellationToken);
            var externalUsersToWhatsapp = await Context.ProjectStakeholderExternalUsers.AsNoTracking()
                                                                               .Where(e => externalUserAssigned.Contains(e.ProjectStakeholderId) && e.ProjectExternalUser.CommunicationTypes.Any(f => f.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp))
                                                                               .Include(e => e.ProjectExternalUser)
                                                                               //.Select(e => e.ProjectExternalUser.CellNumber)
                                                                               .ToListAsync(cancellationToken);

            Dictionary<string, int> ExternalUserNumbers = new Dictionary<string, int>();

            externalUsersToWhatsapp.ForEach(f =>
            {
                ExternalUserNumbers.AddIfNotContainsKey(f.ProjectExternalUser.CellNumber, f.ProjectStakeholderId);
            });

            //Email
            var emailTemplate = await Context.CommunicationTemplates
                                                        .Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.StoreIncidentUpdated &&
                                                                    e.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                        .FirstOrDefaultAsync(cancellationToken);

            //emails.RemoveIfContains(project.Owner.Email); 
            emails.AddIfNotContains(project.Owner.Email);
            if (emailTemplate != null)
            {
                foreach (var stakeholder in AssigneeEmailStakeholderIds)
                {
                    var templateData = new
                    {
                        StoreName = project.Store.Name,
                        PriorityName = project.ProjectPriority.Name,
                        UserEmail = comment.CreatedBy ?? "External User",
                        TypeName = project.ProjectType.Name,
                        SubTypeName = project.ProjectSubType.Name,
                        CategoryName = project.ProjectCategory.Name,
                        SubCategoryName = project.ProjectSubCategory.Name,
                        SupplierName = string.Join(", ", project.ProjectSuppliers.Select(s => s.Supplier.Name).ToList()),
                        AssignedName = string.Join(", ", emails),
                        ShortDescription = project.Name,
                        Comment = comment.Comment,
                        Iid = project.ProjectId,
                        Sid = stakeholder.Value
                    };

                    await Mediator.Send(new CommunicationHistoryProjectInsertCommand
                    {
                        ProjectId = project.ProjectId,
                        CommunicationTemplateId = emailTemplate.CommunicationTemplateId,
                        ToEmail = project.Owner.Email,
                        FromEmail = emailTemplate.FromEmailAddress,
                        FromName = emailTemplate.FromName,
                        Subject = emailTemplate.Subject,
                        Body = emailTemplate.Body,
                        CcEmails = emails.Count > 0 ? string.Join(", ", emails) : null,
                        TemplateData = templateData,
                        AttachmentUrls = command.HasFiles ? string.Join(", ", comment.Files.Select(f => f.Url).ToList()) : "",
                    }, cancellationToken);

                    await Mediator.Send(new SendEmailCommand
                    {
                        ToEmailAddress = stakeholder.Key,
                        FromEmailAddress = emailTemplate.FromEmailAddress,
                        FromEmailName = emailTemplate.FromName,
                        Subject = emailTemplate.Subject,
                        Body = emailTemplate.Body,
                        TemplateData = new
                        {
                            StoreName = project.Store.Name,
                            PriorityName = project.ProjectPriority.Name,
                            UserEmail = comment.CreatedBy ?? "External User",
                            TypeName = project.ProjectType.Name,
                            SubTypeName = project.ProjectSubType.Name,
                            CategoryName = project.ProjectCategory.Name,
                            SubCategoryName = project.ProjectSubCategory.Name,
                            SupplierName = string.Join(", ", project.ProjectSuppliers.Select(s => s.Supplier.Name).ToList()),
                            AssignedName = string.Join(", ", emails),
                            ShortDescription = project.Name,
                            Comment = comment.Comment,
                            Iid = project.ProjectId,
                            Sid = stakeholder.Value
                        },
                        AttachmentUrls = comment.Files != null ? comment.Files.Select(f => f.Url).ToList() : null,
                    }, cancellationToken);
                }
            }

            //WhatsApp
            var whatsAppTemplate = await Context.CommunicationTemplates
                                                        .Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.StoreIncidentUpdated &&
                                                                    e.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp)
                                                        .FirstOrDefaultAsync(cancellationToken);
            if (ExternalUserNumbers.Any())
            {
                foreach (var externalUserNumber in ExternalUserNumbers)
                {
                    var convertedNumber = string.Join("", externalUserNumber.Key.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                    convertedNumber = convertedNumber.Length == 10 && convertedNumber[0] == '0' ? string.Concat("+27", convertedNumber.AsSpan(1)) : convertedNumber;
                    if (whatsAppTemplate != null)
                    {
                        await Mediator.Send(new SendWhatsAppCommand
                        {
                            ToMobileNumbers = [convertedNumber],
                            FromMobileNumber = whatsAppTemplate.FromMobileNumber,
                            Message = whatsAppTemplate.Body,
                            ExternalTemplateId = whatsAppTemplate.ExternalTemplateId,
                            TemplateData = new
                            {
                                StoreName = project.Store.Name,
                                PriorityName = project.ProjectPriority.Name,
                                UserEmail = comment.CreatedBy ?? "External User",
                                TypeName = project.ProjectType.Name,
                                SubTypeName = project.ProjectSubType.Name,
                                CategoryName = project.ProjectCategory.Name,
                                SubCategoryName = project.ProjectSubCategory.Name,
                                SupplierName = string.Join(", ", project.ProjectSuppliers.Select(s => s.Supplier.Name).ToList()),
                                AssignedName = string.Join(", ", emails),
                                ShortDescription = project.Name,
                                Comment = comment.Comment,
                            },
                            MediaUrls = comment.Files != null ? comment.Files.Select(f => f.Url).ToList() : null,
                            Parameters = new Dictionary<string, string>
                            {
                                {"userEmail", project.CreatedBy ?? "External User"},
                                {"storeName", project.Store.Name},
                                {"priorityName", project.ProjectPriority.Name},
                                {"typeName", project.ProjectType.Name + " | " + project.ProjectSubType.Name},
                                //{"subTypeName", project.ProjectSubType.Name},
                                {"categoryName", project.ProjectCategory.Name + " | " + project.ProjectSubCategory.Name},
                                //{"subCategoryName", project.ProjectSubCategory.Name},
                                {"supplierName", string.Join(", ", project.ProjectSuppliers.Select(s => s.Supplier.Name).ToList())},
                                {"statusName", project.ProjectStatus.Name},
                                {"assignedName", string.Join(", ", emails)},
                                {"shortDescription", project.Name},
                                {"comment", comment.Comment},
                                {"iid", project.ProjectId.ToString()},
                                {"sid", externalUserNumber.Value.ToString()}
                            },
                        }, cancellationToken);
                    }
                }
            }

        }

        return new OperationStatus { Status = true, OperationId = command.ProjectCommentId };
    }

}

public class ProjectCommentSendCommunicationCommandValidator : AbstractValidator<ProjectCommentSendCommunicationCommand>
{
    public ProjectCommentSendCommunicationCommandValidator()
    {
        RuleFor(e => e.ProjectCommentId).NotEmpty().GreaterThan(0);
    }
}