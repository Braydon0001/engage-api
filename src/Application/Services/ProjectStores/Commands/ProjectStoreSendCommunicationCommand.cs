using Engage.Application.Services.CommunicationHistoryProjects.Commands;
using Engage.Application.Services.Emails.Commands;
using Engage.Application.Services.WhatsApp.Commands;
using Engage.Application.Services.WhatsAppHistories.Commands;

namespace Engage.Application.Services.ProjectStores.Commands;

public class ProjectStoreSendCommunicationCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public bool SaveFiles { get; set; }
}

public record ProjectStoreSendCommunicationCommandHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStoreSendCommunicationCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreSendCommunicationCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.Include(p => p.Store)
                                                 .Include(p => p.Owner)
                                                 .Include(p => p.ProjectStatus)
                                                 .Include(p => p.ProjectPriority)
                                                 .Include(p => p.ProjectType)
                                                 .Include(p => p.ProjectSubType)
                                                 .Include(p => p.ProjectCategory)
                                                 .Include(p => p.ProjectSubCategory)
                                                 .Include(p => p.ProjectSuppliers)
                                                    .ThenInclude(s => s.Supplier)
                                                 .Include(p => p.ProjectComments)
                                                 .SingleOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken);
        if (project == null)
        {
            return null;
        }

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

        var usersToWhatsApp = await Context.ProjectStakeholderUsers.AsNoTracking()
                                                                   .Where(e => usersAssigned.Contains(e.ProjectStakeholderId) && e.User.UserCommunicationTypes.Any(f => f.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp))
                                                                   .Include(e => e.User)
                                                                   .ToListAsync(cancellationToken);

        var externalUsersToWhatsapp = await Context.ProjectStakeholderExternalUsers.AsNoTracking()
                                                                               .Where(e => externalUserAssigned.Contains(e.ProjectStakeholderId) && e.ProjectExternalUser.CommunicationTypes.Any(f => f.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp))
                                                                               .Include(e => e.ProjectExternalUser)
                                                                               //.Select(e => e.ProjectExternalUser.CellNumber)
                                                                               .ToListAsync(cancellationToken);

        var storeContatsToWhatsApp = await Context.ProjectStakeholderStoreContacts.AsNoTracking()
                                                                                  .Where(e => storeContactAssigned.Contains(e.ProjectStakeholderId) && e.StoreContact.CommunicationTypes.Any(f => f.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp))
                                                                                  .Include(e => e.StoreContact)
                                                                                  .ToListAsync(cancellationToken);

        var supplierContatsToWhatsApp = await Context.ProjectStakeholderSupplierContacts.AsNoTracking()
                                                                                  .Where(e => supplierContactAssigned.Contains(e.ProjectStakeholderId) && e.SupplierContact.CommunicationTypes.Any(f => f.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp))
                                                                                  .Include(e => e.SupplierContact)
                                                                                  .ToListAsync(cancellationToken);

        Dictionary<string, int> UserNumbers = new();

        usersToWhatsApp.ForEach(f =>
        {
            UserNumbers.AddIfNotContainsKey(f.User.MobilePhone, f.ProjectStakeholderId);
        });

        Dictionary<string, int> ExternalUserNumbers = new();

        externalUsersToWhatsapp.ForEach(f =>
        {
            ExternalUserNumbers.AddIfNotContainsKey(f.ProjectExternalUser.CellNumber, f.ProjectStakeholderId);
        });

        Dictionary<string, int> StoreContactNumbers = new();

        storeContatsToWhatsApp.ForEach(f =>
        {
            StoreContactNumbers.AddIfNotContainsKey(f.StoreContact.MobilePhone, f.ProjectStakeholderId);
        });

        Dictionary<string, int> SupplierContactNumbers = new();

        supplierContatsToWhatsApp.ForEach(f =>
        {
            SupplierContactNumbers.AddIfNotContainsKey(f.SupplierContact.MobilePhone, f.ProjectStakeholderId);
        });

        //Email
        var templateEmail = await Context.CommunicationTemplates
                                                    .Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.StoreIncidentUpdated &&
                                                                e.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                    .FirstOrDefaultAsync(cancellationToken);

        emails.RemoveIfContains(project.Owner.Email);
        if (templateEmail != null)
        {
            var latestComment = project.ProjectComments.Any() ? project.ProjectComments.LastOrDefault() : null;
            foreach (var stakeholder in AssigneeEmailStakeholderIds)
            {
                var templateData = new
                {
                    StoreName = project.Store.Name,
                    PriorityName = project.ProjectPriority.Name,
                    UserEmail = project.CreatedBy ?? "External User",
                    TypeName = project.ProjectType.Name,
                    SubTypeName = project.ProjectSubType.Name,
                    CategoryName = project.ProjectCategory.Name,
                    SubCategoryName = project.ProjectSubCategory.Name,
                    SupplierName = string.Join(", ", project.ProjectSuppliers.Select(s => s.Supplier.Name).ToList()),
                    AssignedName = emails.Count > 0 ? string.Join(", ", emails) : "Owner",
                    ShortDescription = project.Name,
                    Comment = latestComment != null ? latestComment.Comment : "",
                    StatusName = project.ProjectStatus.Name,
                    Iid = project.ProjectId,
                    Sid = stakeholder.Value
                };
                //Save History
                await Mediator.Send(new CommunicationHistoryProjectInsertCommand
                {
                    ProjectId = command.ProjectId,
                    CommunicationTemplateId = templateEmail.CommunicationTemplateId,
                    ToEmail = project.Owner.Email,
                    FromEmail = templateEmail.FromEmailAddress,
                    FromName = templateEmail.FromName,
                    Subject = templateEmail.Subject,
                    Body = templateEmail.Body,
                    CcEmails = emails.Count > 0 ? string.Join(", ", emails) : null,
                    TemplateData = templateData,
                    AttachmentUrls = command.SaveFiles && latestComment != null ? string.Join(", ", latestComment.Files.Select(f => f.Url).ToList()) : null,
                }, cancellationToken);

                await Mediator.Send(new SendEmailCommand
                {
                    ToEmailAddress = stakeholder.Key,
                    FromEmailAddress = templateEmail.FromEmailAddress,
                    FromEmailName = templateEmail.FromName,
                    //CcEmailAddresses = emails.Count > 0 ? emails.Distinct().ToList() : new List<string>(),
                    Subject = templateEmail.Subject,
                    Body = templateEmail.Body,
                    TemplateData = templateData,
                    AttachmentUrls = command.SaveFiles && latestComment != null ? latestComment.Files.Select(f => f.Url).ToList() : null,
                }, cancellationToken);
            }
        }

        //WhatsApp
        var templateWhatsApp = await Context.CommunicationTemplates
                                                    .Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.StoreIncidentUpdated &&
                                                                e.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp)
                                                    .FirstOrDefaultAsync(cancellationToken);

        Dictionary<string, int> CombinedNumbers = new();

        void AddDictionary(Dictionary<string, int> source)
        {
            foreach (var kvp in source)
            {
                CombinedNumbers.TryAdd(kvp.Key, kvp.Value);
            }
        }

        AddDictionary(UserNumbers);
        AddDictionary(ExternalUserNumbers);
        AddDictionary(StoreContactNumbers);
        AddDictionary(SupplierContactNumbers);

        if (CombinedNumbers != null && CombinedNumbers.Count != 0)
        {
            foreach (var number in CombinedNumbers)
            {
                //var convertedNumbers = externalUserNumbers.Select(number => number.Length == 10 && number[0] == '0' ? "+27" + number.Substring(1) : number)
                //                                      .ToList();
                var latestComment = project.ProjectComments != null && project.ProjectComments.Count != 0 ? project.ProjectComments.LastOrDefault() : null;
                var convertedNumber = string.Join("", number.Key.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                convertedNumber = convertedNumber.Length == 10 && convertedNumber[0] == '0' ? string.Concat("+27", convertedNumber.AsSpan(1)) : convertedNumber;

                var parameters = new Dictionary<string, string>
                        {
                            {"userEmail", project.CreatedBy ?? "External User"},
                            {"storeName", project.Store.Name},
                            {"priorityName", project.ProjectPriority.Name},
                            {"typeName", project.ProjectType.Name + " | " + project.ProjectSubType.Name},
                            //{"subTypeName", project.ProjectSubType.Name},
                            {"categoryName", project.ProjectCategory.Name + " | " + project.ProjectSubCategory.Name},
                            //{"subCategoryName", project.ProjectSubCategory.Name},
                            {"supplierName", project.ProjectSuppliers.Any() ? string.Join(", ", project.ProjectSuppliers.Select(s => s.Supplier.Name).ToList()) : "No Supplier(s)"},
                            {"statusName", project.ProjectStatus.Name},
                            {"assignedName",emails.Count > 0 ? string.Join(", ", emails) : "Owner"},
                            {"shortDescription",string.IsNullOrEmpty( project.Name)?"No Description":project.Name},
                            {"comment", latestComment != null ? latestComment.Comment : "No Comment Provided"},
                            {"iid", project.ProjectId.ToString()},
                            {"sid", number.Value.ToString()}
                        };

                //Save History
                await Mediator.Send(new WhatsAppHistoryInsertCommand
                {
                    ToMobileNumber = convertedNumber,
                    FromMobileNumber = templateWhatsApp.FromMobileNumber,
                    FromName = templateWhatsApp.FromName,
                    ContentVariables = JsonConvert.SerializeObject(parameters),
                    ExternalTemplateId = templateWhatsApp.ExternalTemplateId,
                }, cancellationToken);

                if (templateWhatsApp != null)
                {
                    await Mediator.Send(new SendWhatsAppCommand
                    {
                        ToMobileNumbers = [convertedNumber],
                        FromMobileNumber = templateWhatsApp.FromMobileNumber,
                        Message = templateWhatsApp.Body,
                        ExternalTemplateId = templateWhatsApp.ExternalTemplateId,
                        TemplateData = new
                        {
                            StoreName = project.Store.Name,
                            PriorityName = project.ProjectPriority.Name,
                            UserEmail = project.CreatedBy ?? "External User",
                            TypeName = project.ProjectType.Name + " | " + project.ProjectSubType.Name,
                            CategoryName = project.ProjectCategory.Name + " | " + project.ProjectSubCategory.Name,
                            SupplierName = project.ProjectSuppliers.Any() ? string.Join(", ", project.ProjectSuppliers.Select(s => s.Supplier.Name).ToList()) : "No Supplier(s)",
                            AssignedName = emails.Count > 0 ? string.Join(", ", emails) : "Owner",
                            StatusName = project.ProjectStatus.Name,
                            ShortDescription = string.IsNullOrEmpty(project.Name) ? "No Description" : project.Name,
                            Comment = latestComment != null ? latestComment.Comment : "No Comment Provided",
                            Iid = project.ProjectId,
                            Sid = number.Value,
                        },
                        MediaUrls = latestComment != null && command.SaveFiles ? latestComment.Files.Select(e => e.Url).ToList() : null,
                        Parameters = parameters,
                    }, cancellationToken);
                }
            }
        }

        return new OperationStatus { Status = true };
    }

}

public class ProjectStoreSendCommunicationCommandValidator : AbstractValidator<ProjectStoreSendCommunicationCommand>
{
    public ProjectStoreSendCommunicationCommandValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
    }
}