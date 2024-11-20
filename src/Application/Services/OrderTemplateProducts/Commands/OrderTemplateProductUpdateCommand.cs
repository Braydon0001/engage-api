namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductUpdateCommand : IMapTo<OrderTemplateProduct>, IRequest<OrderTemplateProduct>
{
    public int Id { get; set; }
    public int Order { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PromotionPrice { get; set; }
    public decimal RecommendedPrice { get; set; }
    public decimal GrossProfitPercent { get; set; }
    public string Suffix { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateProductUpdateCommand, OrderTemplateProduct>();
    }
}

public class OrderTemplateProductUpdateHandler : UpdateHandler, IRequestHandler<OrderTemplateProductUpdateCommand, OrderTemplateProduct>
{
    public OrderTemplateProductUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateProduct> Handle(OrderTemplateProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleOrDefaultAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateOrderTemplateProductValidator : AbstractValidator<OrderTemplateProductUpdateCommand>
{
    public UpdateOrderTemplateProductValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Order).NotEmpty();
        RuleFor(e => e.Quantity).NotEmpty();
        RuleFor(e => e.Price).NotEmpty();
        RuleFor(e => e.PromotionPrice).NotEmpty();
        RuleFor(e => e.RecommendedPrice).NotEmpty();
        RuleFor(e => e.GrossProfitPercent).NotEmpty();
        RuleFor(e => e.Suffix).MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}