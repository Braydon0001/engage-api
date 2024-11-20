using Engage.Application.Services.EntityContacts.Models;

namespace Engage.Application.Services.Orders.Commands;

public class UpdateOrderStatusCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int OrderStatusId { get; set; }
    public bool SaveChanges { get; set; } = true;
    public List<StoreContactEmailOption> EmailAddresses { get; set; }
}

public class UpdateOrderStatusCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateOrderStatusCommand, OperationStatus>
{
    private readonly IUserService _user;
    public UpdateOrderStatusCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var entity = await OrderUtils.GetOrder(_context, command.Id);

        // Unsubmitted to Submitted
        if (entity.OrderStatusId == 1 && command.OrderStatusId == 2)
        {
            entity.OrderStatusId = 2;
            entity.SubmittedDate = DateTime.Now;
        }
        // Submitted to Processed
        else if ((entity.OrderStatusId == 2 && command.OrderStatusId == 3))
        {
            entity.OrderStatusId = 3;
            entity.ProcessedDate = DateTime.Now;
            entity.ProcessedBy = _user.UserName;
        }
        else if (entity.OrderStatusId == 2 && command.OrderStatusId == 2)
        {
            throw new Exception($"This order has already been submitted");
        }
        else
        {
            throw new Exception($"Invalid order status update. " +
                $"Order id: {command.Id}. " +
                $"Current status id: {entity.OrderStatusId}. " +
                $"New status id: {command.OrderStatusId}.");
        }

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.OrderId;
            return opStatus;
        }

        return new OperationStatus
        {
            Status = true
        };
    }
}
