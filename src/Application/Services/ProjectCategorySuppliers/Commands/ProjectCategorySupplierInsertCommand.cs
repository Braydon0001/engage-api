namespace Engage.Application.Services.ProjectCategorySuppliers.Commands;

public class ProjectCategorySupplierInsertCommand : IMapTo<ProjectCategorySupplier>, IRequest<ProjectCategorySupplier>
{
    public int ProjectCategoryId { get; init; }
    public int SupplierId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategorySupplierInsertCommand, ProjectCategorySupplier>();
    }
}

public record ProjectCategorySupplierInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategorySupplierInsertCommand, ProjectCategorySupplier>
{
    public async Task<ProjectCategorySupplier> Handle(ProjectCategorySupplierInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectCategorySupplierInsertCommand, ProjectCategorySupplier>(command);
        
        Context.ProjectCategorySuppliers.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectCategorySupplierInsertValidator : AbstractValidator<ProjectCategorySupplierInsertCommand>
{
    public ProjectCategorySupplierInsertValidator()
    {
        RuleFor(e => e.ProjectCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(0);
    }
}