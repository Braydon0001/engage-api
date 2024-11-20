namespace Engage.Application.Services.ProjectTaskStatuses.Commands;

public class ProjectTaskStatusInsertCommand : IMapTo<ProjectTaskStatus>, IRequest<ProjectTaskStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskStatusInsertCommand, ProjectTaskStatus>();
    }
}

public record ProjectTaskStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskStatusInsertCommand, ProjectTaskStatus>
{
    public async Task<ProjectTaskStatus> Handle(ProjectTaskStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskStatusInsertCommand, ProjectTaskStatus>(command);
        
        Context.ProjectTaskStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTaskStatusInsertValidator : AbstractValidator<ProjectTaskStatusInsertCommand>
{
    public ProjectTaskStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}