using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.Projects.Commands;

public class ProjectInsertCommand : IMapTo<Project>, IRequest<OperationStatus>
{
    public string Name { get; init; }
    //public List<JsonText> Note { get; init; }
    public int ProjectTypeId { get; init; }
    public int ProjectPriorityId { get; init; }
    public int? EngageRegionId { get; init; }
    public int? ProjectCampaignId { get; init; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; init; }
    public float? EstimatedHours { get; init; }
    public float? RemainingHours { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectInsertCommand, Project>();
    }
}

public record ProjectInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IOptions<EngagementSettings> Options) : IRequestHandler<ProjectInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectInsertCommand, Project>(command);

        entity.ProjectStatusId = (int)ProjectStatusId.Unassigned;
        Context.Projects.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status)
        {
            //assign the user to the project
            var user = await Context.Users.Where(u => u.Email.ToLower() == entity.CreatedBy.ToLower()).FirstOrDefaultAsync(cancellationToken);

            if (user != null)
            {
                var projectUser = new ProjectStakeholderUser
                {
                    ProjectId = entity.ProjectId,
                    UserId = user.UserId,
                };

                Context.ProjectStakeholderUsers.Add(projectUser);

                await Context.SaveChangesAsync(cancellationToken);
            }

            var defaultTo = Options.Value.DefaultToEmail;
            var projectTemplate = await Context.CommunicationTemplates.Where(e => e.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.NewProject)
                                                                      .FirstOrDefaultAsync(cancellationToken);

            if (projectTemplate != null)
            {
                await Mediator.Send(new SendEmailCommand
                {
                    ToEmailAddress = defaultTo,
                    FromEmailAddress = projectTemplate.FromEmailAddress,
                    FromEmailName = projectTemplate.FromName,
                    Subject = projectTemplate.Subject,
                    Body = projectTemplate.Body,
                    TemplateData = new
                    {
                        ProjectName = entity.Name,
                        UserEmail = entity.CreatedBy
                    }
                }, cancellationToken);
            }

            #region Uncomment below if we're using Front-End Email Service

            //var template = Mapper.Map<ProjectEmailVm>(projectTemplate);

            //if (projectTemplate != null)
            //{
            //    var defaultTo = Options.Value.DefaultToEmail;

            //    if (command.EngageRegionId.HasValue)
            //    {
            //        var tacOpsIds = await Context.ProjectTacOpRegions.Where(e => e.EngageRegionId == command.EngageRegionId.Value)
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
            //                //remove toEmails and defaultTo from the tacOps list
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

        return opStatus;
    }
}

public class ProjectInsertValidator : AbstractValidator<ProjectInsertCommand>
{
    public ProjectInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        //RuleFor(e => e.Note);
        RuleFor(e => e.ProjectTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectPriorityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageRegionId);
        RuleFor(e => e.ProjectCampaignId);
        RuleFor(e => e.StartDate);
        RuleFor(e => e.EndDate);
        RuleFor(e => e.EstimatedHours);
        RuleFor(e => e.RemainingHours);
    }
}