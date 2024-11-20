using Engage.Application.Services.OrderSkus.Commands;

namespace Engage.Application.Services.Orders.Commands;

public class AddOrderProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SubWarehouse { get; set; }
}

public class AddOrderProductCommand : IRequest<OperationStatus>
{
    public int OrderId { get; set; }
    public int DistributionCenterId { get; set; }
    public int EngageVariantProductId { get; set; }
    public int? DcProductId { get; set; }
}

public class AddOrderProductsCommand : IRequest<OperationStatus>
{
    public int OrderId { get; set; }
    public int DistributionCenterId { get; set; }
    public List<int> EngageVariantProductIds { get; set; }
    public int? DcProductId { get; set; }
}

public class AddOrderProductCommandHandler : BaseCreateCommandHandler, IRequestHandler<AddOrderProductCommand, OperationStatus>
{
    public AddOrderProductCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(AddOrderProductCommand command, CancellationToken cancellationToken)
    {
        List<DCProduct> products;

        if (command.DcProductId.HasValue)
        {
            products = await _context.DCProducts
                                .Include(e => e.UnitType)
                                .Where(e => e.DCProductId == command.DcProductId.Value)
                                .ToListAsync(cancellationToken);
        }
        else
        {
            products = await _context.DCProducts
                                    .Include(e => e.UnitType)
                                    .Where(e =>
                                        e.EngageVariantProductId == command.EngageVariantProductId &&
                                        e.DistributionCenterId == command.DistributionCenterId)
                                    .ToListAsync(cancellationToken);

            // There are multiple Dc Products for the Variant product.   
            // Just return the list of Dc Products     
            if (products.Count > 1)
            {
                var dcProducts = new List<AddOrderProductDto>();
                foreach (var product in products)
                {
                    dcProducts.Add(new AddOrderProductDto()
                    {
                        Id = product.DCProductId,
                        Name = product.Code + " / " + product.Name + " / " + product.Size + " " + product.UnitType.Name,
                        SubWarehouse = product.SubWarehouse
                    });
                }

                return new OperationStatus()
                {
                    Message = "MultipleDCProducts",
                    ReturnObject = dcProducts
                };
            }
        }

        var dcProduct = products.FirstOrDefault();
        if (dcProduct != null)
        {
            var duplicateProduct = _context.OrderSkus.Where(e => e.OrderId == command.OrderId &&
                                                                 e.DCProductId == dcProduct.DCProductId)
                                                     .Any();
            if (duplicateProduct)
            {
                return new OperationStatus()
                {
                    Message = "DuplicateDCProduct",
                    ReturnObject = dcProduct
                };
            }

            await _mediator.Send(new CreateOrderSkuCommand()
            {
                OrderId = command.OrderId,
                OrderSkuTypeId = 1,
                OrderSkuStatusId = 1,
                DCProductId = dcProduct.DCProductId,
                OrderQuantityTypeId = 3, // case
                Quantity = 0,
                SaveChanges = false
            });
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.OrderId;
        return opStatus;
    }
}

public class AddOrderProductsCommandHandler : BaseCreateCommandHandler, IRequestHandler<AddOrderProductsCommand, OperationStatus>
{
    public AddOrderProductsCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(AddOrderProductsCommand command, CancellationToken cancellationToken)
    {
        List<DCProduct> products;

        //if (command.DcProductId.HasValue)
        //{
        //    products = await _context.DCProducts
        //                        .Include(e => e.UnitType)
        //                        .WhereEnabled()
        //                        .Where(e => e.DCProductId == command.DcProductId.Value)
        //                        .ToListAsync(cancellationToken);
        //}
        //else
        //{
        products = await _context.DCProducts
                                .Include(e => e.UnitType)
                                .Where(e =>
                                    // e.EngageVariantProductId == command.EngageVariantProductId &&
                                    e.DistributionCenterId == command.DistributionCenterId)
                                .ToListAsync(cancellationToken);

        // There are multiple Dc Products for /one or more/ variant products.   
        // Just return the list of Dc Products     
        if (products.Count > 1)
        {
            var dcProducts = new List<AddOrderProductDto>();
            foreach (var product in products)
            {
                dcProducts.Add(new AddOrderProductDto()
                {
                    Id = product.DCProductId,
                    Name = product.Code + " / " + product.Name + " / " + product.Size + " " + product.UnitType.Name,
                    SubWarehouse = product.SubWarehouse
                });
            }

            return new OperationStatus()
            {
                Message = "MultipleDCProducts",
                ReturnObject = dcProducts
            };
        }
        //}

        var dcProduct = products.FirstOrDefault();
        if (dcProduct != null)
        {
            var duplicateProduct = _context.OrderSkus.Where(e => e.OrderId == command.OrderId &&
                                                                 e.DCProductId == dcProduct.DCProductId)
                                                     .Any();
            if (duplicateProduct)
            {
                return new OperationStatus()
                {
                    Message = "DuplicateDCProduct",
                    ReturnObject = dcProduct
                };
            }

            await _mediator.Send(new CreateOrderSkuCommand()
            {
                OrderId = command.OrderId,
                OrderSkuTypeId = 1,
                OrderSkuStatusId = 1,
                DCProductId = dcProduct.DCProductId,
                Quantity = 0,
                SaveChanges = false
            });
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.OrderId;
        return opStatus;
    }
}
