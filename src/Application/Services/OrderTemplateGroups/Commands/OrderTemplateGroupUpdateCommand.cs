// auto-generated
namespace Engage.Application.Services.OrderTemplateGroups.Commands;

public class OrderTemplateGroupUpdateCommand : IMapTo<OrderTemplateGroup>, IRequest<OrderTemplateGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateGroupUpdateCommand, OrderTemplateGroup>();
    }
}

public class OrderTemplateGroupUpdateHandler : UpdateHandler, IRequestHandler<OrderTemplateGroupUpdateCommand, OrderTemplateGroup>
{
    public OrderTemplateGroupUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateGroup> Handle(OrderTemplateGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateGroups.SingleOrDefaultAsync(e => e.OrderTemplateGroupId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateOrderTemplateGroupValidator : AbstractValidator<OrderTemplateGroupUpdateCommand>
{
    public UpdateOrderTemplateGroupValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(100);
    }
}