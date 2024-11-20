// auto-generated
namespace Engage.Application.Services.InventoryTransactions.Commands;

public class InventoryTransactionFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class InventoryTransactionFileDeleteHandler : FileDeleteHandler, IRequestHandler<InventoryTransactionFileDeleteCommand, bool>
{
    public InventoryTransactionFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(InventoryTransactionFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryTransactions.SingleOrDefaultAsync(e => e.InventoryTransactionId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(InventoryTransaction), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class InventoryTransactionFileDeleteValidator : FileDeleteValidator<InventoryTransactionFileDeleteCommand>
{
    public InventoryTransactionFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}