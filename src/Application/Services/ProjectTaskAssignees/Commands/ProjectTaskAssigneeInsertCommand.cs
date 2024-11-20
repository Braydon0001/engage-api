namespace Engage.Application.Services.ProjectTaskAssignees.Commands;

public class ProjectTaskAssigneeInsertCommand : IMapTo<ProjectTaskAssignee>, IRequest<ProjectTaskAssignee>
{
    public int ProjectTaskId { get; init; }
    public int ProjectStakeholderId { get; init; }
    public int? ProjectTaskStatusId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskAssigneeInsertCommand, ProjectTaskAssignee>();
    }
}

public record ProjectTaskAssigneeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskAssigneeInsertCommand, ProjectTaskAssignee>
{
    public async Task<ProjectTaskAssignee> Handle(ProjectTaskAssigneeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskAssigneeInsertCommand, ProjectTaskAssignee>(command);
        
        Context.ProjectTaskAssignees.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTaskAssigneeInsertValidator : AbstractValidator<ProjectTaskAssigneeInsertCommand>
{
    public ProjectTaskAssigneeInsertValidator()
    {
        RuleFor(e => e.ProjectTaskId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectStakeholderId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskStatusId);
    }
}