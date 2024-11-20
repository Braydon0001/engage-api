// auto-generated
namespace Engage.Application.Services.InventoryUnitTypes.Commands;

public class InventoryUnitTypeInsertCommand : IMapTo<InventoryUnitType>, IRequest<InventoryUnitType>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryUnitTypeInsertCommand, InventoryUnitType>();
    }
}

public class InventoryUnitTypeInsertHandler : InsertHandler, IRequestHandler<InventoryUnitTypeInsertCommand, InventoryUnitType>
{
    public InventoryUnitTypeInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryUnitType> Handle(InventoryUnitTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<InventoryUnitTypeInsertCommand, InventoryUnitType>(command);
        
        _context.InventoryUnitTypes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryUnitTypeInsertValidator : AbstractValidator<InventoryUnitTypeInsertCommand>
{
    public InventoryUnitTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
    }
}