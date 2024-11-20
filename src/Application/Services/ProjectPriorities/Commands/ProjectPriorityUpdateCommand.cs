namespace Engage.Application.Services.ProjectPriorities.Commands;

public class ProjectPriorityUpdateCommand : IMapTo<ProjectPriority>, IRequest<ProjectPriority>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public bool IsEndDateRequired { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectPriorityUpdateCommand, ProjectPriority>();
    }
}

public record ProjectPriorityUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectPriorityUpdateCommand, ProjectPriority>
{
    public async Task<ProjectPriority> Handle(ProjectPriorityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectPriorities.SingleOrDefaultAsync(e => e.ProjectPriorityId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectPriorityValidator : AbstractValidator<ProjectPriorityUpdateCommand>
{
    public UpdateProjectPriorityValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}