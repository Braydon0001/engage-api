namespace Engage.Application.Services.ProjectCategorySuppliers.Commands;

public class ProjectCategorySupplierUpdateCommand : IRequest<OperationStatus>
{
    public int ProjectCategoryId { get; init; }
    public List<int> SupplierIds { get; init; }
}

public record ProjectCategorySupplierUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectCategorySupplierUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectCategorySupplierUpdateCommand command, CancellationToken cancellationToken)
    {
        var category = await Context.ProjectCategories.FirstOrDefaultAsync(e => e.ProjectCategoryId == command.ProjectCategoryId, cancellationToken)
                    ?? throw new Exception("Category Not Found");

        var categorySuppliers = await Context.ProjectCategorySuppliers.Where(e => e.ProjectCategoryId == command.ProjectCategoryId).ToListAsync(cancellationToken);

        var catSuppliersToRemove = categorySuppliers.Where(e => !command.SupplierIds.Contains(e.SupplierId)).ToList();

        var catSuppliersToAdd = command.SupplierIds.Where(e => !categorySuppliers.Select(e => e.SupplierId).Contains(e)).ToList();

        if (catSuppliersToRemove.Count > 0)
        {
            Context.ProjectCategorySuppliers.RemoveRange(catSuppliersToRemove);
        }

        if (catSuppliersToAdd.Count > 0)
        {
            Context.ProjectCategorySuppliers.AddRange(catSuppliersToAdd.Select(e => new ProjectCategorySupplier
            {
                ProjectCategoryId = command.ProjectCategoryId,
                SupplierId = e
            }));
        }

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class UpdateProjectCategorySupplierValidator : AbstractValidator<ProjectCategorySupplierUpdateCommand>
{
    public UpdateProjectCategorySupplierValidator()
    {
        RuleFor(e => e.ProjectCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierIds).NotEmpty();
    }
}