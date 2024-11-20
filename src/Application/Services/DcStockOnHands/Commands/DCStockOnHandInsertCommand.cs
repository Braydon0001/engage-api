namespace Engage.Application.Services.DCStockOnHands.Commands;

public class DCStockOnHandInsertCommand : IMapTo<DCStockOnHand>, IRequest<DCStockOnHand>
{
    public int DcProductId { get; init; }
    public float OnOrderQuantity { get; init; }
    public DateTime StockDate { get; init; }
    public float Value { get; init; }
    public string Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCStockOnHandInsertCommand, DCStockOnHand>();
    }
}

public record DCStockOnHandInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<DCStockOnHandInsertCommand, DCStockOnHand>
{
    public async Task<DCStockOnHand> Handle(DCStockOnHandInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<DCStockOnHandInsertCommand, DCStockOnHand>(command);
        
        Context.DCStockOnHands.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class DCStockOnHandInsertValidator : AbstractValidator<DCStockOnHandInsertCommand>
{
    public DCStockOnHandInsertValidator()
    {
        RuleFor(e => e.DcProductId).GreaterThan(0);
        RuleFor(e => e.OnOrderQuantity).NotEmpty();
        RuleFor(e => e.StockDate).NotEmpty();
        RuleFor(e => e.Value).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(200);
    }
}