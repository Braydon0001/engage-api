namespace Engage.Application.Services.ProjectDcProducts.Commands;

public class ProjectDcProductInsertCommand : IMapTo<ProjectDcProduct>, IRequest<ProjectDcProduct>
{
    public int ProjectId { get; init; }
    public int DcProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDcProductInsertCommand, ProjectDcProduct>();
    }
}

public record ProjectDcProductInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectDcProductInsertCommand, ProjectDcProduct>
{
    public async Task<ProjectDcProduct> Handle(ProjectDcProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectDcProductInsertCommand, ProjectDcProduct>(command);

        Context.ProjectDcProducts.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectDcProductInsertValidator : AbstractValidator<ProjectDcProductInsertCommand>
{
    public ProjectDcProductInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DcProductId).NotEmpty().GreaterThan(0);
    }
}