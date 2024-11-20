using Engage.Application.Services.Mobile.Orders.Models;


namespace Engage.Application.Services.Mobile.Orders.Queries;

public class GetMobileOrderHistoryQuery : IRequest<List<MobileOrderDto>>
{
    public int EmployeeId { get; set; }
    public string StoreSearch { get; set; }
}

public class GetMobileOrderHistoryQueryHandler : BaseQueryHandler, IRequestHandler<GetMobileOrderHistoryQuery, List<MobileOrderDto>>
{
    public GetMobileOrderHistoryQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<List<MobileOrderDto>> Handle(GetMobileOrderHistoryQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeId == request.EmployeeId);

        if (user == null)
            return new List<MobileOrderDto>();

        var username = string.IsNullOrEmpty(user.EmailAddress1) == false
                                    ? user.EmailAddress1
                                    : string.IsNullOrEmpty(user.EmailAddress2) == false
                                        ? user.EmailAddress2
                                        : "";

        var entities =  _context.Orders
            .Where(o => o.CreatedBy == username)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new MobileOrderDto
            {
                OrderId = o.OrderId,
                StoreName = o.Store.Name,
                StoreImageUrl = o.Store.StoreImageUrl,
                Status = o.OrderStatus.Name,
                Reference = o.OrderReference,
                OrderDate = o.OrderDate.ToString("dd MMM yyy"),
                DeliveryDate = o.DeliveryDate.HasValue ? o.DeliveryDate.Value.ToString("dd MMM yyy") : "",
                Products = o.OrderSkus
                    .Select(or => new MobileOrderProductDto
                    {
                        OrderSkuId = or.OrderSkuId,
                        ProductCode = or.DCProduct.Code,
                        ProductName = or.DCProduct.Name,
                        Quantity = or.Quantity,
                        QuantityType = or.OrderQuantityType.Name,
                        UnitType = or.DCProduct.Size.ToString() + " " + or.DCProduct.UnitType.Name,
                        Warehouse = or.DCProduct.SubWarehouse
                    }).ToList()
            }).AsQueryable();
           
        if (request.StoreSearch != null)
        {
            entities = entities.Where(x => (EF.Functions.Like(x.StoreName, $"%{request.StoreSearch}%")));
        }
        var data = await entities.Take(100)
            .ToListAsync(cancellationToken);


        return data;
    }
}
