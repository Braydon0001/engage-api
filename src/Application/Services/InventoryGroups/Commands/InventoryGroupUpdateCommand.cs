// auto-generated
namespace Engage.Application.Services.InventoryGroups.Commands;

public class InventoryGroupUpdateCommand : IMapTo<InventoryGroup>, IRequest<InventoryGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryGroupUpdateCommand, InventoryGroup>();
    }
}

public class InventoryGroupUpdateHandler : UpdateHandler, IRequestHandler<InventoryGroupUpdateCommand, InventoryGroup>
{
    public InventoryGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryGroup> Handle(InventoryGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryGroups.SingleOrDefaultAsync(e => e.InventoryGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateInventoryGroupValidator : AbstractValidator<InventoryGroupUpdateCommand>
{
    public UpdateInventoryGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
    }
}