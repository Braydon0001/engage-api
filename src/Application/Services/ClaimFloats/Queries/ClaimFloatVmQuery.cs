using Engage.Application.Services.ClaimFloats.Models;

namespace Engage.Application.Services.ClaimFloats.Queries
{
    public class ClaimFloatVmQuery: GetByIdQuery, IRequest<ClaimFloatVm>
    {
    }

    public class ClaimFloatVmQueryHandler : BaseQueryHandler, IRequestHandler<ClaimFloatVmQuery, ClaimFloatVm>
    {
        public ClaimFloatVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ClaimFloatVm> Handle(ClaimFloatVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.ClaimFloats.Include(e => e.EngageRegion)
                                                   .Include(e => e.Supplier)
                                                   .SingleAsync(e => e.ClaimFloatId == request.Id, cancellationToken);

            var vm = _mapper.Map<ClaimFloat, ClaimFloatVm>(entity);

            var claims = await _context.ClaimFloatClaims
                                        .Include(c => c.Claim)
                                        .ThenInclude(c => c.ClaimSkus)
                                        .Where(c => c.ClaimFloatId == request.Id)
                                        .ToListAsync();

            decimal totalClaims = 0;

            if (claims.Count > 0)
            {
                foreach(var claim in claims)
                {
                    totalClaims = totalClaims + claim.Claim.ClaimSkus.Sum(s => s.TotalAmount);
                }
            }

            vm.RemainingAmount = vm.Amount - totalClaims;

            return vm;
        }
    }
}
