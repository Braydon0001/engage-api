// auto-generated
namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductInsertCommand : IMapTo<OrderTemplateProduct>, IRequest<OrderTemplateProduct>
{
    public int OrderTemplateId { get; set; }
    public int OrderTemplateGroupId { get; set; }
    public int DcProductId { get; set; }
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
        profile.CreateMap<OrderTemplateProductInsertCommand, OrderTemplateProduct>();
    }
}

public class OrderTemplateProductInsertHandler : InsertHandler, IRequestHandler<OrderTemplateProductInsertCommand, OrderTemplateProduct>
{
    public OrderTemplateProductInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateProduct> Handle(OrderTemplateProductInsertCommand command, CancellationToken cancellationToken)
    {
        var orderGroups = await _context.OrderTemplateGroups.Where(e => e.OrderTemplateId == command.OrderTemplateId)
                                                            .Select(e => e.OrderTemplateGroupId)
                                                            .ToListAsync(cancellationToken);
        if (orderGroups == null)
        {
            throw new Exception("Order Template Group not found");
        }

        var previousDcProduct = await _context.OrderTemplateProducts
                                              .Where(e => e.DCProductId == command.DcProductId
                                                    && orderGroups.Contains(e.OrderTemplateGroupId))
                                              .FirstOrDefaultAsync(cancellationToken);
        if (previousDcProduct != null)
        {
            if (previousDcProduct.Disabled == true)
            {
                previousDcProduct.Disabled = false;
                previousDcProduct.OrderTemplateGroupId = command.OrderTemplateGroupId;
                await _context.SaveChangesAsync(cancellationToken);
                return previousDcProduct;
            }
            throw new Exception("Product already added to template");
        }

        var entity = _mapper.Map<OrderTemplateProductInsertCommand, OrderTemplateProduct>(command);

        _context.OrderTemplateProducts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class OrderTemplateProductInsertValidator : AbstractValidator<OrderTemplateProductInsertCommand>
{
    public OrderTemplateProductInsertValidator()
    {
        RuleFor(e => e.OrderTemplateId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.OrderTemplateGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DcProductId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Order).GreaterThanOrEqualTo(0);
        RuleFor(e => e.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(e => e.Price).GreaterThanOrEqualTo(0);
        RuleFor(e => e.PromotionPrice).GreaterThanOrEqualTo(0);
        RuleFor(e => e.RecommendedPrice).GreaterThanOrEqualTo(0);
        RuleFor(e => e.GrossProfitPercent).GreaterThanOrEqualTo(0);
        RuleFor(e => e.Suffix).MaximumLength(100);
        RuleFor(e => e.Note).MaximumLength(1000);
    }
}