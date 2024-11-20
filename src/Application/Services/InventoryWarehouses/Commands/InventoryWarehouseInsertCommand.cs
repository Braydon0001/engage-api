// auto-generated
namespace Engage.Application.Services.InventoryWarehouses.Commands;

public class InventoryWarehouseInsertCommand : IMapTo<InventoryWarehouse>, IRequest<InventoryWarehouse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryWarehouseInsertCommand, InventoryWarehouse>();
    }
}

public class InventoryWarehouseInsertHandler : InsertHandler, IRequestHandler<InventoryWarehouseInsertCommand, InventoryWarehouse>
{
    public InventoryWarehouseInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryWarehouse> Handle(InventoryWarehouseInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<InventoryWarehouseInsertCommand, InventoryWarehouse>(command);
        
        _context.InventoryWarehouses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryWarehouseInsertValidator : AbstractValidator<InventoryWarehouseInsertCommand>
{
    public InventoryWarehouseInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
    }
}