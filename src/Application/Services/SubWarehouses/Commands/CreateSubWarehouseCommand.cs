using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Models;
using Engage.Application.Services.Shared.Commands;
using Engage.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.SubWarehouses.Commands
{
    public class CreateSubWarehouseCommand : SubWarehouseCommand, IRequest<OperationStatus>
    {
        
    }

    public class CreateSubWarehouseCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSubWarehouseCommand, OperationStatus>
    {
        public CreateSubWarehouseCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(CreateSubWarehouseCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateSubWarehouseCommand, SubWarehouse>(request);

            _context.SubWarehouses.Add(entity);

            var operationStatus = await _context.SaveChangesAsync(cancellationToken);
            operationStatus.OperationId = entity.SubWarehouseId;
            return operationStatus; 
        }
    }
}
