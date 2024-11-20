namespace Engage.Application.Services.ProjectStatuses.Commands;

public class ProjectStatusUpdateCommand : IMapTo<ProjectStatus>, IRequest<ProjectStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStatusUpdateCommand, ProjectStatus>();
    }
}

public record ProjectStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStatusUpdateCommand, ProjectStatus>
{
    public async Task<ProjectStatus> Handle(ProjectStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectStatuses.SingleOrDefaultAsync(e => e.ProjectStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectStatusValidator : AbstractValidator<ProjectStatusUpdateCommand>
{
    public UpdateProjectStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}