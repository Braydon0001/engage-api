namespace Engage.Application.Services.OrderSkus.Commands
{
    public class CreateOrderSkuWithDateCommand : OrderSkuCommand, IRequest<OperationStatus>
    {
        public int OrderId { get; set; }
        public bool SaveChanges { get; set; } = true;
        public DateTime? DeliveryDate { get; set; }
    }

    public class CreateOrderSkuWithDateCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateOrderSkuWithDateCommand, OperationStatus>
    {
        public CreateOrderSkuWithDateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateOrderSkuWithDateCommand command, CancellationToken cancellationToken)
        {
            if (command.DeliveryDate.HasValue)
            {
                if (command.DeliveryDate.Value.Date < DateTime.Now.Date)
                {
                    throw new Exception("Delivery date cannot be in the past.");
                }
            }

            var existing = await _context.OrderSkus.FirstOrDefaultAsync(e => e.OrderId == command.OrderId &&
                                                                        e.DCProductId == command.DCProductId &&
                                                                        e.DeliveryDate.Value.Date == command.DeliveryDate.Value.Date &&
                                                                        e.Deleted == false &&
                                                                        e.Disabled == false);
            if (existing != null)
            {
                return new OperationStatus
                {
                    Status = true
                };
            }

            var entity = _mapper.Map<CreateOrderSkuWithDateCommand, OrderSku>(command);
            entity.OrderId = command.OrderId;
            entity.DeliveryDate = command.DeliveryDate;
            _context.OrderSkus.Add(entity);

            if (command.SaveChanges)
            {
                var opStatus = await _context.SaveChangesAsync(cancellationToken);
                opStatus.OperationId = entity.OrderSkuId;
                return opStatus;
            }
            return new OperationStatus
            {
                Status = true
            };

        }
    }
}
