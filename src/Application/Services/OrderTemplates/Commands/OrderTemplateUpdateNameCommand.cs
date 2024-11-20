namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateUpdateNameCommand : IRequest<OrderTemplate>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class OrderTemplateUpdateNameHandler : IRequestHandler<OrderTemplateUpdateNameCommand, OrderTemplate>
{
    private readonly IAppDbContext _context;

    public OrderTemplateUpdateNameHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderTemplate> Handle(OrderTemplateUpdateNameCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplates.SingleOrDefaultAsync(e => e.OrderTemplateId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.Name = command.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderTemplateUpdateNameValidator : AbstractValidator<OrderTemplateUpdateNameCommand>
{
    public OrderTemplateUpdateNameValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}