namespace Engage.Application.Services.Inventories.Commands;

public class InventoryUpdateCommand : IMapTo<Inventory>, IRequest<Inventory>
{
    public int Id { get; set; }
    public int InventoryGroupId { get; init; }
    public int InventoryStatusId { get; init; }
    public int InventoryUnitTypeId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string BarCode { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryUpdateCommand, Inventory>();
    }
}

public record InventoryUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryUpdateCommand, Inventory>
{
    public async Task<Inventory> Handle(InventoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Inventories.SingleOrDefaultAsync(e => e.InventoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryValidator : AbstractValidator<InventoryUpdateCommand>
{
    public UpdateInventoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryUnitTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
        RuleFor(e => e.BarCode).MaximumLength(100);
    }
}