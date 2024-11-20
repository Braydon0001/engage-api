namespace Engage.Application.Services.ProjectTypes.Commands;

public class ProjectTypeInsertCommand : IMapTo<ProjectType>, IRequest<ProjectType>
{
    public string Name { get; init; }
    public bool IsDescriptionRequired { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTypeInsertCommand, ProjectType>();
    }
}

public record ProjectTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTypeInsertCommand, ProjectType>
{
    public async Task<ProjectType> Handle(ProjectTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectTypeInsertCommand, ProjectType>(command);

        Context.ProjectTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectTypeInsertValidator : AbstractValidator<ProjectTypeInsertCommand>
{
    public ProjectTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}