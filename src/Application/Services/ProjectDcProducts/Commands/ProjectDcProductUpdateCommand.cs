namespace Engage.Application.Services.ProjectDcProducts.Commands;

public class ProjectDcProductUpdateCommand : IMapTo<ProjectDcProduct>, IRequest<ProjectDcProduct>
{
    public int Id { get; set; }
    public int ProjectId { get; init; }
    public int DcProductId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDcProductUpdateCommand, ProjectDcProduct>();
    }
}

public record ProjectDcProductUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectDcProductUpdateCommand, ProjectDcProduct>
{
    public async Task<ProjectDcProduct> Handle(ProjectDcProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectDcProducts.SingleOrDefaultAsync(e => e.ProjectDcProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectDcProductValidator : AbstractValidator<ProjectDcProductUpdateCommand>
{
    public UpdateProjectDcProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DcProductId).NotEmpty().GreaterThan(0);
    }
}