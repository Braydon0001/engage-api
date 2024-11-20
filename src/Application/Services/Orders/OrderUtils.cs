namespace Engage.Application.Services.Orders;

static class OrderUtils
{
    static public async Task<Order> GetOrder(IAppDbContext context, int Id)
    {
        var entity = await context.Orders
            .FirstOrDefaultAsync(x => x.OrderId == Id);

        return entity == null ? throw new NotFoundException(nameof(Orders), Id) : entity;
    }
}
