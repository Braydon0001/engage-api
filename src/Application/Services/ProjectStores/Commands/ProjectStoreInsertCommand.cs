using Engage.Application.Services.ProjectAssignees.Commands;
using Engage.Application.Services.ProjectEngageBrands.Commands;
using Engage.Application.Services.ProjectStakeholders.Commands;

namespace Engage.Application.Services.ProjectStores.Commands;

public class ProjectStoreInsertCommand : IMapTo<ProjectStore>, IRequest<OperationStatus>
{
    public string Description { get; set; }
    public int ProjectStoreId { get; set; }
    public int ProjectTypeId { get; set; }
    public int ProjectSubTypeId { get; set; }
    public List<int> DcProductIds { get; set; }
    public List<int> StoreAssetIds { get; set; }
    public int ProjectCategoryId { get; set; }
    public int? ProjectSubCategoryId { get; set; }
    public List<int> SupplierIds { get; set; }
    public List<int> EngageBrandIds { get; set; }
    public List<StakeholderIds> ProjectStakeholderIds { get; set; }
    public List<StakeholderIds> ProjectAssignedTo { get; set; }
    public int ProjectPriorityId { get; set; }
    public string ProjectComment { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreInsertCommand, ProjectStore>()
                .ForMember(s => s.Name, opt => opt.MapFrom(s => s.Description))
                .ForMember(s => s.StoreId, opt => opt.MapFrom(s => s.ProjectStoreId));
    }
}

public class ProjectStoreInsertHandler : InsertHandler, IRequestHandler<ProjectStoreInsertCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IOptions<EngagementSettings> _options;
    private readonly IUserService _user;
    public ProjectStoreInsertHandler(IAppDbContext context, IMapper mapper, IMediator Mediator, IOptions<EngagementSettings> options, IUserService userService) : base(context, mapper)
    {
        _mediator = Mediator;
        _options = options;
        _user = userService;
    }
    public async Task<OperationStatus> Handle(ProjectStoreInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProjectStoreInsertCommand, ProjectStore>(command);

        ProjectComment comment = new();

        var user = await _context.Users.Where(e => e.Email.ToLower() == _user.UserName.ToLower()).FirstOrDefaultAsync(cancellationToken)
            ?? throw new Exception("User not found");

        entity.ProjectStatusId = (int)ProjectStatusId.Assigned;
        entity.OwnerId = user.UserId;
        _context.ProjectStores.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status)
        {
            ProjectTask task = new()
            {
                Name = "",
                ProjectId = entity.ProjectId,
                ProjectTaskStatusId = (int)ProjectTaskStatusId.Assigned,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                OpenedBy = _user.UserName,
                EstimatedHours = 0,
                RemainingHours = 0,
            };

            _context.ProjectTasks.Add(task);

            //Save Stakeholders
            if (command.ProjectStakeholderIds.IsNotNullOrEmpty())
            {
                await _mediator.Send(new ProjectStakeholderCreateCommand
                {
                    ProjectId = entity.ProjectId,
                    ProjectStakeholderIds = command.ProjectStakeholderIds,
                    ProjectAssignedIds = command.ProjectAssignedTo,
                    Save = false
                }, cancellationToken);
            }
            //var creatorStakeholder = new ProjectStakeholderUser
            //{
            //    ProjectId = entity.ProjectId,
            //    UserId = user.UserId
            //};
            //_context.ProjectStakeholderUsers.Add(creatorStakeholder);

            // DCProducts
            if (command.DcProductIds.IsNotNullOrEmpty())
            {
                foreach (var product in command.DcProductIds)
                {
                    _context.ProjectDcProducts.Add(new ProjectDcProduct
                    {
                        ProjectId = entity.ProjectId,
                        DcProductId = product
                    });
                }
            }

            // Store Assets
            if (command.StoreAssetIds.IsNotNullOrEmpty())
            {
                foreach (var storeAsset in command.StoreAssetIds)
                {
                    _context.ProjectStoreAssets.Add(new ProjectStoreAsset
                    {
                        ProjectId = entity.ProjectId,
                        StoreAssetId = storeAsset
                    });
                }
            }

            // Suppliers
            if (command.SupplierIds.IsNotNullOrEmpty())
            {
                foreach (var supplier in command.SupplierIds)
                {
                    _context.ProjectSuppliers.Add(new ProjectSupplier
                    {
                        ProjectId = entity.ProjectId,
                        SupplierId = supplier
                    });
                }
            }

            if (command.EngageBrandIds.IsNotNullOrEmpty())
            {
                await _mediator.Send(new ProjectEngageBrandUpdateCommand
                {
                    ProjectId = entity.ProjectId,
                    EngageBrandIds = command.EngageBrandIds,
                    Save = false
                }, cancellationToken);
            }

            // Comment
            if (command.ProjectComment.IsNotNullOrEmpty())
            {
                comment = new ProjectComment
                {
                    ProjectId = entity.ProjectId,
                    Comment = command.ProjectComment,
                    ProjectStatusId = entity.ProjectStatusId
                };
                _context.ProjectComments.Add(comment);
            }

            await _context.SaveChangesAsync(cancellationToken);

            //Assign to creator
            var result = await _mediator.Send(new ProjectAssigneeInsertCommand
            {
                ProjectId = entity.ProjectId,
                ProjectAssigneeIds = command.ProjectAssignedTo,
                Save = false
            }, cancellationToken);


            //Send Email
            //var defaultTo = Options.Value.DefaultToEmail;
            //List<string> ccEmails = new List<string>();
            //var projectTemplate = await _context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.NewStoreProject)
            //                                                          .FirstOrDefaultAsync(cancellationToken);

            //var store = await _context.Stores.Include(e => e.EngageRegion)
            //                                .Where(e => e.StoreId == command.StoreId)
            //                                .FirstOrDefaultAsync(cancellationToken);

            //var tacOpsIds = await _context.ProjectTacOpRegions.Where(e => e.EngageRegionId == store.EngageRegionId)
            //                                                 .Select(e => e.ProjectTacOpId)
            //                                                 .ToListAsync(cancellationToken);

            //if (tacOpsIds != null && tacOpsIds.Count > 0)
            //{
            //    var tacOps = await _context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
            //                                            .Select(e => e.User.Email)
            //                                            .Distinct()
            //                                            .ToListAsync(cancellationToken);

            //    if (tacOps != null && tacOps.Count > 0)
            //    {
            //        defaultTo = tacOps.Select(e => e).First();
            //        tacOps.Remove(defaultTo);
            //        if (tacOps.Count > 0)
            //        {
            //            ccEmails.AddRange(tacOps);
            //        }
            //    }
            //}

            //if (projectTemplate != null)
            //{
            //    await Mediator.Send(new SendEmailCommand
            //    {
            //        ToEmailAddress = defaultTo,
            //        FromEmailAddress = projectTemplate.FromEmailAddress,
            //        FromEmailName = projectTemplate.FromEmailName,
            //        Subject = projectTemplate.Subject,
            //        Body = projectTemplate.Body,
            //        CcEmailAddresses = ccEmails,
            //        TemplateData = new
            //        {
            //            ProjectName = entity.Name,
            //            UserEmail = entity.CreatedBy,
            //            StoreName = $"{store.Name} - {store.EngageRegion.Name}",
            //        }
            //    }, cancellationToken);
            //}

            await _context.SaveChangesAsync(cancellationToken);

            #region Uncomment below if we're using Front-End Email Service
            //var projectTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.NewStoreProject)
            //                                                          .FirstOrDefaultAsync(cancellationToken);

            //var template = Mapper.Map<ProjectEmailVm>(projectTemplate);

            //if (projectTemplate != null)
            //{
            //    var defaultTo = Options.Value.DefaultToEmail;

            //    var store = await Context.Stores.Where(e => e.StoreId == command.StoreId)
            //                                       .FirstOrDefaultAsync(cancellationToken);

            //    template.StoreName = store.Name;

            //    var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == store.EngageRegionId)
            //                                                     .Select(e => e.ProjectTacOpId)
            //                                                     .ToListAsync(cancellationToken);

            //    if (tacOpsIds != null && tacOpsIds.Count > 0)
            //    {
            //        var tacOps = await Context.ProjectTacOps.Where(e => tacOpsIds.Contains(e.ProjectTacOpId))
            //                                                .Select(e => e.User.Email)
            //                                                .Distinct()
            //                                                .ToListAsync(cancellationToken);

            //        var toEmail = tacOps.Select(e => e).First();

            //        if (tacOps != null && tacOps.Count > 0)
            //        {
            //            tacOps.Remove(toEmail);
            //            tacOps.Remove(defaultTo);

            //            template.ToEmailAddress = toEmail;
            //            if (tacOps.Count > 0)
            //            {
            //                if (template.CcEmails == null)
            //                {
            //                    template.CcEmails = tacOps;
            //                }
            //                else
            //                {
            //                    template.CcEmails.AddRange(tacOps);
            //                }
            //            }
            //        }
            //    }

            //    template.ToEmailAddress = template.ToEmailAddress ?? defaultTo;
            //    template.CreatedBy = entity.CreatedBy;

            //    //do not return the template if the toEmailAddress is null
            //    if (template.ToEmailAddress != null)
            //    {
            //        opStatus.ReturnObject = template;
            //    }
            //}
            #endregion
        }

        opStatus.OperationId = entity.ProjectId;

        if (comment != null)
        {
            opStatus.ReturnObject = comment.ProjectCommentId;
        }

        return opStatus;
    }
}

public class ProjectStoreInsertValidator : AbstractValidator<ProjectStoreInsertCommand>
{
    public ProjectStoreInsertValidator()
    {
        RuleFor(e => e.Description).MaximumLength(100);
        RuleFor(e => e.ProjectStoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectPriorityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectSubTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectSubCategoryId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
    }
}
