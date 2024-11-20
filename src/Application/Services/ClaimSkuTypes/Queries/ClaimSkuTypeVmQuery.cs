using Engage.Application.Services.ClaimSkuTypes.Models;

namespace Engage.Application.Services.ClaimSkuTypes.Queries
{
    public class ClaimSkuTypeVmQuery : GetByIdQuery, IRequest<ClaimSkuTypeVm>
    {
    }

    public class ClaimSkuTypeVMQueryHandler : BaseQueryHandler, IRequestHandler<ClaimSkuTypeVmQuery, ClaimSkuTypeVm>
    {
        public ClaimSkuTypeVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ClaimSkuTypeVm> Handle(ClaimSkuTypeVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimSkuTypes.SingleAsync(x => x.ClaimSkuTypeId == request.Id, cancellationToken);
            return _mapper.Map<ClaimSkuType, ClaimSkuTypeVm>(entity);
        }
    }
}
