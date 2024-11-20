// auto-generated
namespace Engage.Application.Services.OrderTemplateGroups.Commands;

public class OrderTemplateGroupInsertCommand : IMapTo<OrderTemplateGroup>, IRequest<OrderTemplateGroup>
{
    public int OrderTemplateId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateGroupInsertCommand, OrderTemplateGroup>();
    }
}

public class OrderTemplateGroupInsertHandler : InsertHandler, IRequestHandler<OrderTemplateGroupInsertCommand, OrderTemplateGroup>
{
    public OrderTemplateGroupInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateGroup> Handle(OrderTemplateGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<OrderTemplateGroupInsertCommand, OrderTemplateGroup>(command);

        _context.OrderTemplateGroups.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderTemplateGroupInsertValidator : AbstractValidator<OrderTemplateGroupInsertCommand>
{
    public OrderTemplateGroupInsertValidator()
    {
        RuleFor(e => e.OrderTemplateId).GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Description).MaximumLength(100);
    }
}