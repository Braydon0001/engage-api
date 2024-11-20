namespace Engage.Application.Services.ProjectStatuses.Commands;

public class ProjectStatusInsertCommand : IMapTo<ProjectStatus>, IRequest<ProjectStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStatusInsertCommand, ProjectStatus>();
    }
}

public record ProjectStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStatusInsertCommand, ProjectStatus>
{
    public async Task<ProjectStatus> Handle(ProjectStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectStatusInsertCommand, ProjectStatus>(command);
        
        Context.ProjectStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectStatusInsertValidator : AbstractValidator<ProjectStatusInsertCommand>
{
    public ProjectStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}