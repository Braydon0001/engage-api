using Engage.Application.Services.ClaimPeriods.Models;

namespace Engage.Application.Services.ClaimPeriods.Queries;

public class ClaimPeriodVmQuery : GetByIdQuery, IRequest<ClaimPeriodVm>
{
}

public class GetClaimPeriodVmQueryHandler : BaseQueryHandler, IRequestHandler<ClaimPeriodVmQuery, ClaimPeriodVm>
{
    public GetClaimPeriodVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ClaimPeriodVm> Handle(ClaimPeriodVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimPeriods.Include(e => e.ClaimYear)
                                                .SingleOrDefaultAsync(x => x.ClaimPeriodId == request.Id, cancellationToken);

        return _mapper.Map<ClaimPeriod, ClaimPeriodVm>(entity);
    }
}
