using AutoMapper;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.SubWarehouses.Models;
using Engage.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.SubWarehouses.Queries
{
    public class SubWarehouseVmQuery : GetByIdQuery, IRequest<SubWarehouseVm>
    {
        
    }

    public class SubWarehouseVmQueryHandler : BaseQueryHandler, IRequestHandler<SubWarehouseVmQuery, SubWarehouseVm>
    {
        public SubWarehouseVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<SubWarehouseVm> Handle(SubWarehouseVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.SubWarehouses.SingleAsync(e => e.SubWarehouseId == request.Id, cancellationToken);

            return _mapper.Map<SubWarehouse, SubWarehouseVm>(entity);
        }
    }
}
