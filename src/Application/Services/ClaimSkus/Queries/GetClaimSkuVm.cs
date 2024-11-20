using Engage.Application.Services.ClaimSkus.Models;

namespace Engage.Application.Services.ClaimSkus.Queries
{
    public class GetClaimSkuVmQuery : GetByIdQuery, IRequest<ClaimSkuVm>
    {
    }

    public class GetClaimSkuVmQueryHandler : BaseQueryHandler, IRequestHandler<GetClaimSkuVmQuery, ClaimSkuVm>
    {
        public GetClaimSkuVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ClaimSkuVm> Handle(GetClaimSkuVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimSkus.Include(e => e.ClaimSkuStatus)
                                                 .Include(e => e.ClaimQuantityType)
                                                 .Include(e => e.ClaimQuantityType)
                                                 .Include(e => e.DCProduct)
                                                 .SingleAsync(e => e.ClaimSkuId == request.Id, cancellationToken);
            return _mapper.Map<ClaimSku, ClaimSkuVm>(entity);
        }
    }
}
