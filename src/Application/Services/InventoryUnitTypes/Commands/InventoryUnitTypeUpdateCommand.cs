// auto-generated
namespace Engage.Application.Services.InventoryUnitTypes.Commands;

public class InventoryUnitTypeUpdateCommand : IMapTo<InventoryUnitType>, IRequest<InventoryUnitType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryUnitTypeUpdateCommand, InventoryUnitType>();
    }
}

public class InventoryUnitTypeUpdateHandler : UpdateHandler, IRequestHandler<InventoryUnitTypeUpdateCommand, InventoryUnitType>
{
    public InventoryUnitTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryUnitType> Handle(InventoryUnitTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryUnitTypes.SingleOrDefaultAsync(e => e.InventoryUnitTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryUnitTypeValidator : AbstractValidator<InventoryUnitTypeUpdateCommand>
{
    public UpdateInventoryUnitTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
    }
}