using Engage.Application.Interfaces;
using Engage.Application.Models;
using Engage.Application.Models.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.OrderSkus.Commands
{
    public class CreateOrderSkuDescriptionCommand: IRequest<OperationStatus>
    {
        public int OrderId { get; set; }        
        public string Description { get; set; }        
    }

    public class CreateOrderSkuDescriptionCommandHandler : IRequestHandler<CreateOrderSkuDescriptionCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;
        private readonly IMediator _mediator;
        private readonly OrderDefaultsOptions _orderDefaults;

        public CreateOrderSkuDescriptionCommandHandler(IAppDbContext context, IMediator mediator, IOptions<OrderDefaultsOptions> options)
        {
            _context = context;
            _mediator = mediator;
            _orderDefaults = options.Value;
        }

        public async Task<OperationStatus> Handle(CreateOrderSkuDescriptionCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateOrderSkuCommand()
            {
                OrderId = command.OrderId,
                OrderSkuTypeId = _orderDefaults.DescriptionSkuTypeId,
                OrderSkuStatusId = 1, 
                OrderQuantityTypeId = _orderDefaults.DescriptionSkuQuantityTypeId, // none
                DCProductId = _orderDefaults.DescriptionSkuDCProductId,  // Description product 
                Quantity = 0,
                Description = command.Description
            });

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.OrderId;
            return opStatus;

        }
    }
}
