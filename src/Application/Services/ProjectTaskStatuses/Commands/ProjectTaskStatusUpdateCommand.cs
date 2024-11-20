namespace Engage.Application.Services.ProjectTaskStatuses.Commands;

public class ProjectTaskStatusUpdateCommand : IMapTo<ProjectTaskStatus>, IRequest<ProjectTaskStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskStatusUpdateCommand, ProjectTaskStatus>();
    }
}

public record ProjectTaskStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskStatusUpdateCommand, ProjectTaskStatus>
{
    public async Task<ProjectTaskStatus> Handle(ProjectTaskStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskStatuses.SingleOrDefaultAsync(e => e.ProjectTaskStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTaskStatusValidator : AbstractValidator<ProjectTaskStatusUpdateCommand>
{
    public UpdateProjectTaskStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}