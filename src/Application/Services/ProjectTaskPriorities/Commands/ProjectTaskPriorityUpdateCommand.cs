namespace Engage.Application.Services.ProjectTaskPriorities.Commands;

public class ProjectTaskPriorityUpdateCommand : IMapTo<ProjectTaskPriority>, IRequest<ProjectTaskPriority>
{
    public int Id { get; set; }
    public int ProjectTaskPriorityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskPriorityUpdateCommand, ProjectTaskPriority>();
    }
}

public record ProjectTaskPriorityUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskPriorityUpdateCommand, ProjectTaskPriority>
{
    public async Task<ProjectTaskPriority> Handle(ProjectTaskPriorityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskPriorities.SingleOrDefaultAsync(e => e.ProjectTaskPriorityId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTaskPriorityValidator : AbstractValidator<ProjectTaskPriorityUpdateCommand>
{
    public UpdateProjectTaskPriorityValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskPriorityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}