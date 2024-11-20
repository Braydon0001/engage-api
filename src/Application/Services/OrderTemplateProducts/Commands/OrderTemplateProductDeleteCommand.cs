namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductDeleteCommand : IRequest<OrderTemplateProduct>
{
    public int Id { get; set; }
}

public class OrderTemplateProductDeleteHandler : IRequestHandler<OrderTemplateProductDeleteCommand, OrderTemplateProduct>
{
    private readonly IAppDbContext _context;

    public OrderTemplateProductDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderTemplateProduct> Handle(OrderTemplateProductDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleOrDefaultAsync(e => e.OrderTemplateProductId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var orderSkusExist = await _context.OrderSkus.Where(e => e.OrderTemplateProductId == query.Id)
                                                     .AnyAsync(cancellationToken);
        if (orderSkusExist)
        {
            entity.Disabled = true;
        }
        else
        {
            _context.OrderTemplateProducts.Remove(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
