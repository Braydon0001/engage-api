namespace Engage.Application.Services.Inventories.Commands;

public class InventoryInsertCommand : IMapTo<Inventory>, IRequest<Inventory>
{
    public int InventoryGroupId { get; init; }
    public int InventoryStatusId { get; init; }
    public int InventoryUnitTypeId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string BarCode { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryInsertCommand, Inventory>();
    }
}

public record InventoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryInsertCommand, Inventory>
{
    public async Task<Inventory> Handle(InventoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<InventoryInsertCommand, Inventory>(command);
        
        Context.Inventories.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryInsertValidator : AbstractValidator<InventoryInsertCommand>
{
    public InventoryInsertValidator()
    {
        RuleFor(e => e.InventoryGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InventoryUnitTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
        RuleFor(e => e.BarCode).MaximumLength(100);
    }
}