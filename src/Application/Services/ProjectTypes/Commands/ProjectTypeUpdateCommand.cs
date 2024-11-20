namespace Engage.Application.Services.ProjectTypes.Commands;

public class ProjectTypeUpdateCommand : IMapTo<ProjectType>, IRequest<ProjectType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public bool IsDescriptionRequired { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTypeUpdateCommand, ProjectType>();
    }
}

public record ProjectTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTypeUpdateCommand, ProjectType>
{
    public async Task<ProjectType> Handle(ProjectTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTypes.SingleOrDefaultAsync(e => e.ProjectTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectTypeValidator : AbstractValidator<ProjectTypeUpdateCommand>
{
    public UpdateProjectTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}