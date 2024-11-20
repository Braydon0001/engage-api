namespace Engage.Application.Services.ProjectTaskTypes.Commands;

public class ProjectTaskTypeUpdateCommand : IMapTo<ProjectTaskType>, IRequest<ProjectTaskType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskTypeUpdateCommand, ProjectTaskType>();
    }
}

public record ProjectTaskTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskTypeUpdateCommand, ProjectTaskType>
{
    public async Task<ProjectTaskType> Handle(ProjectTaskTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskTypes.SingleOrDefaultAsync(e => e.ProjectTaskTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTaskTypeValidator : AbstractValidator<ProjectTaskTypeUpdateCommand>
{
    public UpdateProjectTaskTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}