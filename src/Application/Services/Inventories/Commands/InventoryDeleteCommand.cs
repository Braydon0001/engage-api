namespace Engage.Application.Services.Inventories.Commands;

public record InventoryDeleteCommand(int Id) : IRequest<Inventory>
{
}

public record InventoryDeleteHandler(IAppDbContext Context) : IRequestHandler<InventoryDeleteCommand, Inventory>
{
    public async Task<Inventory> Handle(InventoryDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Inventories.SingleOrDefaultAsync(e => e.InventoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        Context.Inventories.Remove(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}