namespace Engage.Application.Services.ProjectSubTypes.Commands;

public class ProjectSubTypeInsertCommand : IMapTo<ProjectSubType>, IRequest<ProjectSubType>
{
    public int ProjectTypeId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubTypeInsertCommand, ProjectSubType>();
    }
}

public record ProjectSubTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubTypeInsertCommand, ProjectSubType>
{
    public async Task<ProjectSubType> Handle(ProjectSubTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectSubTypeInsertCommand, ProjectSubType>(command);
        
        Context.ProjectSubTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectSubTypeInsertValidator : AbstractValidator<ProjectSubTypeInsertCommand>
{
    public ProjectSubTypeInsertValidator()
    {
        RuleFor(e => e.ProjectTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}