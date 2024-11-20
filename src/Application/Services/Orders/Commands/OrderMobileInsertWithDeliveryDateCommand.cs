using Engage.Application.Services.EntityContacts.Models;
using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.Orders.Commands;

public class OrderMobileInsertWithDeliveryDateCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int DistributionCenterId { get; set; }
    public string Reference { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<int> EngageDepartments { get; set; }
    public List<ProductWithDropsDto> Products { get; set; }
    public List<StoreContactEmailOption> EmailAddresses { get; set; }
}

public class OrderMobileWithDeliveryDateInsertHandler : BaseCreateCommandHandler, IRequestHandler<OrderMobileInsertWithDeliveryDateCommand, OperationStatus>
{
    private readonly IUserService _user;
    private readonly OrderDefaultsOptions _options;

    public OrderMobileWithDeliveryDateInsertHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user, IOptions<OrderDefaultsOptions> options)
        : base(context, mapper, mediator)
    {
        _user = user;
        _options = options.Value;
    }

    public async Task<OperationStatus> Handle(OrderMobileInsertWithDeliveryDateCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeId == request.EmployeeId);

        if (user == null)
            return new OperationStatus { Status = false, Message = "Employee was not found" };

        var userGroups = await _context.UserUserGroups.IgnoreQueryFilters().Include(e => e.UserGroup).Where(e => e.UserId == user.UserId && !e.Deleted && !e.Disabled).Select(e => e.UserGroup.Name).ToListAsync(cancellationToken);

        var hasOrderClaims = userGroups.Contains("order.capture") && userGroups.Contains("order.manage");

        if (!hasOrderClaims)
        {
            throw new Exception("Not authorized to place orders");
        }

        var username = string.IsNullOrEmpty(user.EmailAddress1) == false
                                    ? user.EmailAddress1
                                    : string.IsNullOrEmpty(user.EmailAddress2) == false
                                        ? user.EmailAddress2
                                        : "";

        // Get the DC Account number.
        var dcAccountNo = await _context.DCAccounts
            .Where(e => e.StoreId == request.StoreId &&
                        e.DistributionCenterId == request.DistributionCenterId)
            .OrderByDescending(e => e.IsPrimary)
            .Select(e => e.AccountNumber)
            .FirstOrDefaultAsync(cancellationToken);

        if (dcAccountNo == null)
            return new OperationStatus { Status = false, Message = "The order was not saved. Could not find DC Account number for the store." };

        // Get a list of only the EngageVarientProduct Ids.
        var productIds = request.Products.Select(p => p.EngageVariantProductId).ToList();

        // Fetch all the DC Products linked to the EngageVarientProduct Id from above list.
        var dcProducts = await _context.DCProducts
            .Where(p => productIds.Contains(p.EngageVariantProductId.Value) && p.DistributionCenterId == request.DistributionCenterId &&
                        p.Disabled == false && p.ProductActiveStatusId == _options.ProductActiveStatusId)
            .ToListAsync();

        List<OrderSku> orderSkus = new List<OrderSku>();    // DC products for this order.
        var excludedProducts = new List<string>();          // A list of products that could not be found in the DC.

        foreach (var dtoProduct in request.Products)
        {
            var dcProduct = dcProducts.FirstOrDefault(p => p.EngageVariantProductId == dtoProduct.EngageVariantProductId);

            if (dtoProduct == null)
            {
                // Fetch the EngageVarientProduct from the DB
                var exludedProduct = await _context.EngageVariantProducts
                    .FirstOrDefaultAsync(p => p.EngageVariantProductId == dtoProduct.EngageVariantProductId);
                // Add the Product name to the "Not Found in DC" product list.
                excludedProducts.Add(exludedProduct.Name);
            }
            else
            {
                // Create a OrderSKU and add it to the order.
                if (dtoProduct.Drops.Count > 0)
                {
                    foreach (var drop in dtoProduct.Drops)
                    {
                        orderSkus.Add(new OrderSku
                        {
                            DCProductId = dcProduct.DCProductId,
                            OrderSkuStatusId = 1,
                            OrderSkuTypeId = 1,
                            Note = drop.Note,
                            // default to case if not specified
                            OrderQuantityTypeId = drop.OrderQuantityTypeId == 0 ? 3 : drop.OrderQuantityTypeId,
                            DeliveryDate = drop.DeliveryDate.HasValue ? drop.DeliveryDate.Value : request.DeliveryDate.HasValue ? request.DeliveryDate.Value : DateTime.Now,
                            Quantity = drop.Quantity,
                            CreatedBy = username,
                            Created = DateTime.Now
                        });
                    }
                }

            }
        }

        if (orderSkus.Count == 0)
            return new OperationStatus { Status = false, Message = "The order was not saved. Could not find any of the products within the DC." };


        // Get the Last orderId.
        var orderCount = await _context.Orders.CountAsync();
        var maxValue = orderCount > 1 ? await _context.Orders.IgnoreQueryFilters().MaxAsync(o => o.OrderId) : 0;

        var order = new Order()
        {
            StoreId = request.StoreId,
            SupplierId = _user.SupplierId,
            DistributionCenterId = request.DistributionCenterId,
            OrderReference = request.Reference,
            DeliveryDate = request.DeliveryDate,

            OrderNo = (maxValue + 1).ToString(),
            OrderDate = DateTime.Now,
            DCAccountNo = dcAccountNo,
            OrderStatusId = 2, //Submitted (A Mobile order is automatically submitted on save) 
            SubmittedDate = DateTime.Now,
            OrderTypeId = 2, // Mobile

            OrderSkus = orderSkus,

            CreatedBy = username,
            Created = DateTime.Now
        };

        if (request.EngageDepartments.Count() > 0)
        {
            var departmentId = request.EngageDepartments[0];

            order.OrderEngageDepartments = new List<OrderEngageDepartment>() {
                    new OrderEngageDepartment { EngageDepartmentId = departmentId }
                };
        }

        try
        {

            _context.Orders.Add(order);
            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            if (opStatus.Status == true)
            {
                opStatus.OperationId = order.OrderId;
            }

            return opStatus;
        }
        catch (Exception ex)
        {
            return OperationStatus.CreateFromException("Error while saving mobile order.", ex);
        }
    }
}
