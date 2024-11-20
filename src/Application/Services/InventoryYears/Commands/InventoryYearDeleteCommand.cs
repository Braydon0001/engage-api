namespace Engage.Application.Services.InventoryYears.Commands;

public record InventoryYearDeleteCommand(int Id) : IRequest<InventoryYear>;

public record InventoryYearDeleteHandler(IAppDbContext Context) : IRequestHandler<InventoryYearDeleteCommand, InventoryYear>
{
    public async Task<InventoryYear> Handle(InventoryYearDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.InventoryYears.SingleOrDefaultAsync(e => e.InventoryYearId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        Context.InventoryYears.Remove(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}