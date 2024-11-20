using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Models;
using Engage.Application.Services.Shared.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.SubWarehouses.Commands
{
    public class UpdateSubWarehouseCommand: SubWarehouseCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateSubWarehouseCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSubWarehouseCommand, OperationStatus>
    {
        public UpdateSubWarehouseCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(UpdateSubWarehouseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SubWarehouses.SingleAsync(e => e.SubWarehouseId == request.Id, cancellationToken);

            _mapper.Map(request, entity);

            var operationStatus = await _context.SaveChangesAsync(cancellationToken);
            operationStatus.OperationId = entity.SubWarehouseId;
            return operationStatus;
        }
    }
}
