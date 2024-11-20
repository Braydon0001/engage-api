namespace Engage.Application.Services.Suppliers.Commands;

public class SupplierFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class SupplierFileDeleteHandler : FileDeleteHandler, IRequestHandler<SupplierFileDeleteCommand, bool>
{
    public SupplierFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(SupplierFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Suppliers.SingleOrDefaultAsync(e => e.SupplierId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(Supplier), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class SupplierFileDeleteValidator : FileDeleteValidator<SupplierFileDeleteCommand>
{
    public SupplierFileDeleteValidator()
    {
    }
}