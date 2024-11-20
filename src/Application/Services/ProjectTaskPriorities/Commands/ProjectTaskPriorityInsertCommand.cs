namespace Engage.Application.Services.ProjectTaskPriorities.Commands;

public class ProjectTaskPriorityInsertCommand : IMapTo<ProjectTaskPriority>, IRequest<ProjectTaskPriority>
{
    public int ProjectTaskPriorityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskPriorityInsertCommand, ProjectTaskPriority>();
    }
}

public record ProjectTaskPriorityInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskPriorityInsertCommand, ProjectTaskPriority>
{
    public async Task<ProjectTaskPriority> Handle(ProjectTaskPriorityInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskPriorityInsertCommand, ProjectTaskPriority>(command);
        
        Context.ProjectTaskPriorities.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTaskPriorityInsertValidator : AbstractValidator<ProjectTaskPriorityInsertCommand>
{
    public ProjectTaskPriorityInsertValidator()
    {
        RuleFor(e => e.ProjectTaskPriorityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}