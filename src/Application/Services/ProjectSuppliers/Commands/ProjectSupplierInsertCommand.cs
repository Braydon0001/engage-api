namespace Engage.Application.Services.ProjectSuppliers.Commands;

public class ProjectSupplierInsertCommand : IMapTo<ProjectSupplier>, IRequest<ProjectSupplier>
{
    public int ProjectId { get; init; }
    public int SupplierId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSupplierInsertCommand, ProjectSupplier>();
    }
}

public record ProjectSupplierInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSupplierInsertCommand, ProjectSupplier>
{
    public async Task<ProjectSupplier> Handle(ProjectSupplierInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectSupplierInsertCommand, ProjectSupplier>(command);
        
        Context.ProjectSuppliers.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectSupplierInsertValidator : AbstractValidator<ProjectSupplierInsertCommand>
{
    public ProjectSupplierInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
    }
}