namespace Engage.Application.Services.OrderStagings.Commands;

public class OrderStagingInsertCommand : IMapTo<OrderStaging>, IRequest<OrderStaging>
{
    public string Region { get; init; }
    public string StoreName { get; init; }
    public string AccountNumber { get; init; }
    public string OrderNumber { get; init; }
    public string OrderContactName { get; init; }
    public string OrderContactEmail { get; init; }
    public string VatNumber { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderStagingInsertCommand, OrderStaging>();
    }
}

public record OrderStagingInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingInsertCommand, OrderStaging>
{
    public async Task<OrderStaging> Handle(OrderStagingInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<OrderStagingInsertCommand, OrderStaging>(command);
        
        Context.OrderStagings.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderStagingInsertValidator : AbstractValidator<OrderStagingInsertCommand>
{
    public OrderStagingInsertValidator()
    {
        RuleFor(e => e.Region).MaximumLength(120);
        RuleFor(e => e.StoreName).MaximumLength(120);
        RuleFor(e => e.AccountNumber).MaximumLength(120);
        RuleFor(e => e.OrderNumber).MaximumLength(120);
        RuleFor(e => e.OrderContactName).MaximumLength(120);
        RuleFor(e => e.OrderContactEmail).MaximumLength(120);
        RuleFor(e => e.VatNumber).MaximumLength(120);
    }
}