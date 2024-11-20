namespace Engage.Application.Services.ProjectSubTypes.Commands;

public class ProjectSubTypeUpdateCommand : IMapTo<ProjectSubType>, IRequest<ProjectSubType>
{
    public int Id { get; set; }
    public int ProjectTypeId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubTypeUpdateCommand, ProjectSubType>();
    }
}

public record ProjectSubTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubTypeUpdateCommand, ProjectSubType>
{
    public async Task<ProjectSubType> Handle(ProjectSubTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectSubTypes.SingleOrDefaultAsync(e => e.ProjectSubTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectSubTypeValidator : AbstractValidator<ProjectSubTypeUpdateCommand>
{
    public UpdateProjectSubTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}