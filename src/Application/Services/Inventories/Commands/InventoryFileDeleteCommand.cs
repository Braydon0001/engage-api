namespace Engage.Application.Services.Inventories.Commands;

public class InventoryFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record InventoryFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<InventoryFileDeleteCommand, bool>
{
    public async Task<bool> Handle(InventoryFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Inventories.SingleOrDefaultAsync(e => e.InventoryId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(Inventory), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class InventoryFileDeleteValidator : FileDeleteValidator<InventoryFileDeleteCommand>
{
    public InventoryFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}