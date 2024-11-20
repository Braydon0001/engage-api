namespace Engage.Application.Services.ProjectTaskSeverities.Commands;

public class ProjectTaskSeverityInsertCommand : IMapTo<ProjectTaskSeverity>, IRequest<ProjectTaskSeverity>
{
    public int ProjectTaskSeverityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskSeverityInsertCommand, ProjectTaskSeverity>();
    }
}

public record ProjectTaskSeverityInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskSeverityInsertCommand, ProjectTaskSeverity>
{
    public async Task<ProjectTaskSeverity> Handle(ProjectTaskSeverityInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskSeverityInsertCommand, ProjectTaskSeverity>(command);

        Context.ProjectTaskSeverities.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTaskSeverityInsertValidator : AbstractValidator<ProjectTaskSeverityInsertCommand>
{
    public ProjectTaskSeverityInsertValidator()
    {
        RuleFor(e => e.ProjectTaskSeverityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}