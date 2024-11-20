// auto-generated
namespace Engage.Application.Services.InventoryWarehouses.Commands;

public class InventoryWarehouseUpdateCommand : IMapTo<InventoryWarehouse>, IRequest<InventoryWarehouse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryWarehouseUpdateCommand, InventoryWarehouse>();
    }
}

public class InventoryWarehouseUpdateHandler : UpdateHandler, IRequestHandler<InventoryWarehouseUpdateCommand, InventoryWarehouse>
{
    public InventoryWarehouseUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryWarehouse> Handle(InventoryWarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryWarehouses.SingleOrDefaultAsync(e => e.InventoryWarehouseId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryWarehouseValidator : AbstractValidator<InventoryWarehouseUpdateCommand>
{
    public UpdateInventoryWarehouseValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
    }
}