using AutoMapper;
using AutoMapper.QueryableExtensions;
using Engage.Application.Interfaces;
using Engage.Application.Services.Shared.Models;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.SubWarehouses.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Engage.Application.Services.SubWarehouses.Queries
{
    public class SubWarehousesQuery: IRequest<ListResult<SubWarehouseDto>>
    {        
    }

    public class SubWarehousesQueryHandler : BaseQueryHandler, IRequestHandler<SubWarehousesQuery, ListResult<SubWarehouseDto>>
    {
        public SubWarehousesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<SubWarehouseDto>> Handle(SubWarehousesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.SubWarehouses.OrderBy(e => e.SubWarehouseId)
                                                       .ProjectTo<SubWarehouseDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);
            return new ListResult<SubWarehouseDto>(entities);
        }
    }
}
