namespace Engage.Application.Services.ProjectTaskTypes.Commands;

public class ProjectTaskTypeInsertCommand : IMapTo<ProjectTaskType>, IRequest<ProjectTaskType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskTypeInsertCommand, ProjectTaskType>();
    }
}

public record ProjectTaskTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskTypeInsertCommand, ProjectTaskType>
{
    public async Task<ProjectTaskType> Handle(ProjectTaskTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTaskTypeInsertCommand, ProjectTaskType>(command);
        
        Context.ProjectTaskTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTaskTypeInsertValidator : AbstractValidator<ProjectTaskTypeInsertCommand>
{
    public ProjectTaskTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}