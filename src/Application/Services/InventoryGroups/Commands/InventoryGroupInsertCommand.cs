// auto-generated
namespace Engage.Application.Services.InventoryGroups.Commands;

public class InventoryGroupInsertCommand : IMapTo<InventoryGroup>, IRequest<InventoryGroup>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryGroupInsertCommand, InventoryGroup>();
    }
}

public class InventoryGroupInsertHandler : InsertHandler, IRequestHandler<InventoryGroupInsertCommand, InventoryGroup>
{
    public InventoryGroupInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryGroup> Handle(InventoryGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<InventoryGroupInsertCommand, InventoryGroup>(command);
        
        _context.InventoryGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class InventoryGroupInsertValidator : AbstractValidator<InventoryGroupInsertCommand>
{
    public InventoryGroupInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).NotEmpty().MaximumLength(100);
    }
}