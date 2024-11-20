using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Models;
using Engage.Application.Services.Shared.Commands;
using Engage.Domain.Entities;
using MediatR;

namespace Engage.Application.Services.OrderSkus.Commands
{
    public class CreateOrderSkuCommand : OrderSkuCommand, IRequest<OperationStatus>
    {
        public int OrderId { get; set; }
        public bool SaveChanges { get; set; } = true;
    }

    public class OrderSkuProduct {
        public int DcProductId { get; set; }
        public int OrderQuantityTypeId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }

    } 

    public class CreateOrderSkuCommand2 : IRequest<OperationStatus>
    {
        public int OrderId { get; set; }
        public List<OrderSkuProduct> Products { get; set; }
    }

    public class CreateOrderSkuCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateOrderSkuCommand, OperationStatus>
    {
        public CreateOrderSkuCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateOrderSkuCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateOrderSkuCommand, OrderSku>(command);
            entity.OrderId = command.OrderId;
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
