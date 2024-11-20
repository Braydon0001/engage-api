namespace Engage.Application.Services.ProjectTacopsBoard.Commands;

public class ProjectTaskUpdateSummaryCommand : IMapTo<ProjectTask>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    //public string Note { get; init; }
    public int ProjectId { get; init; }
    public int ProjectTaskStatusId { get; set; }
    public int? ProjectTaskTypeId { get; set; }
    //public int? ProjectTaskSeverityId { get; set; }
    public int ProjectTaskPriorityId { get; set; }
    public int? UserId { get; set; }
    public int? ProjectStakeholderId { get; set; }
    public bool IsClosed { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskUpdateSummaryCommand, ProjectTask>();
    }
}

public record ProjectTaskUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectTaskUpdateSummaryCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectTaskUpdateSummaryCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTasks.SingleOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        //mappedEntity.ProjectStakeholderId = command.ProjectStakeholderId;

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        opStatus.OperationId = entity.ProjectTaskId;
        return opStatus;
    }
}

public class UpdateProjectTaskValidator : AbstractValidator<ProjectTaskUpdateSummaryCommand>
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
        RuleFor(e => e.UserId);
        RuleFor(e => e.ProjectStakeholderId);
    }
}