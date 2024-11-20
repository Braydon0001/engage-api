namespace Engage.Application.Services.ProjectPriorities.Commands;

public class ProjectPriorityInsertCommand : IMapTo<ProjectPriority>, IRequest<ProjectPriority>
{
    public string Name { get; init; }
    public bool IsEndDateRequired { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectPriorityInsertCommand, ProjectPriority>();
    }
}

public record ProjectPriorityInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectPriorityInsertCommand, ProjectPriority>
{
    public async Task<ProjectPriority> Handle(ProjectPriorityInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectPriorityInsertCommand, ProjectPriority>(command);

        Context.ProjectPriorities.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectPriorityInsertValidator : AbstractValidator<ProjectPriorityInsertCommand>
{
    public ProjectPriorityInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}