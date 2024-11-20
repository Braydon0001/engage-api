namespace Engage.Application.Services.DCStockOnHands.Commands;

public class DCStockOnHandUpdateCommand : IMapTo<DCStockOnHand>, IRequest<DCStockOnHand>
{
    public int Id { get; set; }
    public int DcProductId { get; init; }
    public float OnOrderQuantity { get; init; }
    public DateTime StockDate { get; init; }
    public float Value { get; init; }
    public string Note { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DCStockOnHandUpdateCommand, DCStockOnHand>();
    }
}

public record DCStockOnHandUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<DCStockOnHandUpdateCommand, DCStockOnHand>
{
    public async Task<DCStockOnHand> Handle(DCStockOnHandUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.DCStockOnHands.SingleOrDefaultAsync(e => e.DCStockOnHandId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateDCStockOnHandValidator : AbstractValidator<DCStockOnHandUpdateCommand>
{
    public UpdateDCStockOnHandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DcProductId).GreaterThan(0);
        RuleFor(e => e.OnOrderQuantity).NotEmpty();
        RuleFor(e => e.StockDate).NotEmpty();
        RuleFor(e => e.Value).NotEmpty();
        RuleFor(e => e.Note).MaximumLength(200);
    }
}