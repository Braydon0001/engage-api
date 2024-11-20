namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateUpdateStartDateCommand : IRequest<OrderTemplate>
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }

}

public class OrderTemplateUpdateStartDatesHandler : IRequestHandler<OrderTemplateUpdateStartDateCommand, OrderTemplate>
{
    private readonly IAppDbContext _context;

    public OrderTemplateUpdateStartDatesHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderTemplate> Handle(OrderTemplateUpdateStartDateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplates.SingleOrDefaultAsync(e => e.OrderTemplateId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        entity.StartDate = command.StartDate;

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderTemplateUpdateStartDateValidator : AbstractValidator<OrderTemplateUpdateStartDateCommand>
{
    public OrderTemplateUpdateStartDateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotNull();
    }
}