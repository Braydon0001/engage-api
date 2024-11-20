namespace Engage.Application.Services.OrderTemplateGroups.Commands;

public class OrderTemplateGroupDeleteCommand : IRequest<OrderTemplateGroup>
{
    public int Id { get; set; }
}

public class OrderTemplateGroupDeleteHandler : IRequestHandler<OrderTemplateGroupDeleteCommand, OrderTemplateGroup>
{
    private readonly IAppDbContext _context;

    public OrderTemplateGroupDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderTemplateGroup> Handle(OrderTemplateGroupDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateGroups.SingleOrDefaultAsync(e => e.OrderTemplateGroupId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var productsExist = await _context.OrderTemplateProducts.Where(e => e.OrderTemplateGroupId == query.Id)
                                                                .AnyAsync(cancellationToken);

        if (productsExist)
        {
            throw new Exception("This group cannot be deleted because it has products. \n\n Please delete the products first.");
        }

        _context.OrderTemplateGroups.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
