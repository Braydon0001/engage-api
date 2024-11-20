using Engage.Application.Services.ClaimYears.Models;

namespace Engage.Application.Services.ClaimYears.Queries;

public class ClaimYearVmQuery : GetByIdQuery, IRequest<ClaimYearVm>
{
}

public class GetClaimYearVmQueryHandler : BaseQueryHandler, IRequestHandler<ClaimYearVmQuery, ClaimYearVm>
{
    public GetClaimYearVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ClaimYearVm> Handle(ClaimYearVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimYears.SingleOrDefaultAsync(x => x.ClaimYearId == request.Id, cancellationToken);
        return _mapper.Map<ClaimYear, ClaimYearVm>(entity);
    }
}
