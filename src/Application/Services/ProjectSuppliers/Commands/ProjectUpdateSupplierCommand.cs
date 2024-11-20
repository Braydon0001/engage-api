namespace Engage.Application.Services.ProjectSuppliers.Commands;

public class ProjectUpdateSupplierCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<int> SupplierIds { get; init; }
    public bool Save { get; init; } = true;
}

public record ProjectUpdateSupplierHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectUpdateSupplierCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectUpdateSupplierCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken) ?? throw new Exception("No Project Found");

        var projectSuppliers = await Context.ProjectSuppliers.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var suppliersToDelete = projectSuppliers.Where(e => !command.SupplierIds.Contains(e.SupplierId)).ToList();

        var suppliersToAdd = command.SupplierIds.Where(e => !projectSuppliers.Select(e => e.SupplierId).ToList().Contains(e)).ToList();

        if (suppliersToDelete.Count != 0)
        {
            Context.ProjectSuppliers.RemoveRange(suppliersToDelete);
        }

        if (suppliersToAdd.Any())
        {
            Context.ProjectSuppliers.AddRange(suppliersToAdd.Select(e => new ProjectSupplier
            {
                ProjectId = command.ProjectId,
                SupplierId = e
            }));
        }

        OperationStatus status = new();

        if (command.Save)
        {
            status = await Context.SaveChangesAsync(cancellationToken);
        }

        return status;
    }
}

public class ProjectUpdateSupplierValidator : AbstractValidator<ProjectUpdateSupplierCommand>
{
    public ProjectUpdateSupplierValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SupplierIds).NotNull();
    }
}