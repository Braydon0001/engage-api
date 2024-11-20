namespace Engage.Application.Services.ProjectTaskAssignees.Commands;

public class ProjectTaskAssigneeUpdateCommand : IMapTo<ProjectTaskAssignee>, IRequest<ProjectTaskAssignee>
{
    public int Id { get; set; }
    public int ProjectTaskId { get; init; }
    public int ProjectStakeholderId { get; init; }
    public int? ProjectTaskStatusId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskAssigneeUpdateCommand, ProjectTaskAssignee>();
    }
}

public record ProjectTaskAssigneeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskAssigneeUpdateCommand, ProjectTaskAssignee>
{
    public async Task<ProjectTaskAssignee> Handle(ProjectTaskAssigneeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskAssignees.SingleOrDefaultAsync(e => e.ProjectTaskAssigneeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTaskAssigneeValidator : AbstractValidator<ProjectTaskAssigneeUpdateCommand>
{
    public UpdateProjectTaskAssigneeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectStakeholderId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskStatusId);
    }
}