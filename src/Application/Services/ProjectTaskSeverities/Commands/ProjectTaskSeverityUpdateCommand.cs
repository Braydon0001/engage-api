namespace Engage.Application.Services.ProjectTaskSeverities.Commands;

public class ProjectTaskSeverityUpdateCommand : IMapTo<ProjectTaskSeverity>, IRequest<ProjectTaskSeverity>
{
    public int Id { get; set; }
    public int ProjectTaskSeverityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskSeverityUpdateCommand, ProjectTaskSeverity>();
    }
}

public record ProjectTaskSeverityUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskSeverityUpdateCommand, ProjectTaskSeverity>
{
    public async Task<ProjectTaskSeverity> Handle(ProjectTaskSeverityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskSeverities.SingleOrDefaultAsync(e => e.ProjectTaskSeverityId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTaskSeverityValidator : AbstractValidator<ProjectTaskSeverityUpdateCommand>
{
    public UpdateProjectTaskSeverityValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTaskSeverityId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}