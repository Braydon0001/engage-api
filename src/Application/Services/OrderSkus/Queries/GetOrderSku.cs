using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Engage.Application.Services.Shared.Queries;
using Engage.Application.Services.OrderSkus.Models;
using Engage.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Engage.Application.Interfaces;

namespace Engage.Application.Services.OrderSkus.Queries
{
    public class GetOrderSkuQuery : GetByIdQuery, IRequest<OrderSkuDto>
    { }

    public class GetClaimSkuQueryHandler : BaseQueryHandler, IRequestHandler<GetOrderSkuQuery, OrderSkuDto>
    {
        public GetClaimSkuQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OrderSkuDto> Handle(GetOrderSkuQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.OrderSkus.FirstOrDefaultAsync(x => x.OrderSkuId == request.Id, cancellationToken);

            return _mapper.Map<OrderSku, OrderSkuDto>(entity);
        }
    }
}
