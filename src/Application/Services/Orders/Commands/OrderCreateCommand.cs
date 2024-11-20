using Engage.Application.Services.OrderSkus.Commands;
using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Orders.Commands;

public class CreateOrderCommand : OrderCommand, IRequest<OperationStatus>
{
    public string CreatedBy { get; set; }
    public int? DcAccountId { get; set; }
}

public class CreateOrderCommand2 : IRequest<OperationStatus>
{
    public int StoreId { get; set; }
    public int DistributionCenterId { get; set; }
    public int SupplierId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string OrderReference { get; set; }
    public List<OrderSkuProduct> Products { get; set; }
}

public class CreateOrderCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateOrderCommand, OperationStatus>
{
    private readonly IUserService _user;

    public CreateOrderCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user)
        : base(context, mapper, mediator)
    {
        _user = user;
    }

    public async Task<OperationStatus> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {

        if (string.IsNullOrWhiteSpace(command.DCAccountNo))
        {


            if (command.DcAccountId.HasValue)
            {
                var dcAccountNo = await _context.DCAccounts
                                    .Where(e => e.DCAccountId == command.DcAccountId.Value)
                                    .Select(e => e.AccountNumber)
                                    .FirstOrDefaultAsync(cancellationToken);

                command.DCAccountNo = dcAccountNo;
            }
            else
            {
                var dcAccountNo = await _context.DCAccounts
                                        .Where(e => e.StoreId == command.StoreId &&
                                                    e.DistributionCenterId == command.DistributionCenterId &&
                                                    e.Disabled == false)
                                        .Select(e => e.AccountNumber)
                                        .FirstOrDefaultAsync(cancellationToken);

                command.DCAccountNo = dcAccountNo;
            }
        }

        var entity = _mapper.Map<CreateOrderCommand, Order>(command);

        var maxValue = await _context.Orders.IgnoreQueryFilters().MaxAsync(o => o.OrderId, cancellationToken);
        entity.OrderNo = (maxValue + 1).ToString();

        entity.CreatedBy = command.CreatedBy;
        entity.SupplierId = _user.SupplierId;

        _context.Orders.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.OrderId;

            if (command.EngageDepartmentIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(
                    AssignDesc.ENGAGE_DEPARTMENT_ORDER, entity.OrderId, command.EngageDepartmentIds));
            }

        }

        return opStatus;
    }
}
