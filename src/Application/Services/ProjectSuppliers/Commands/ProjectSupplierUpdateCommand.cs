namespace Engage.Application.Services.ProjectSuppliers.Commands;

public class ProjectSupplierUpdateCommand : IMapTo<ProjectSupplier>, IRequest<ProjectSupplier>
{
    public int Id { get; set; }
    public int ProjectId { get; init; }
    public int SupplierId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSupplierUpdateCommand, ProjectSupplier>();
    }
}

public record ProjectSupplierUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSupplierUpdateCommand, ProjectSupplier>
{
    public async Task<ProjectSupplier> Handle(ProjectSupplierUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectSuppliers.SingleOrDefaultAsync(e => e.ProjectSupplierId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectSupplierValidator : AbstractValidator<ProjectSupplierUpdateCommand>
{
    public UpdateProjectSupplierValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
    }
}