using Engage.Application.Services.ClaimNotificationUsers.Models;

namespace Engage.Application.Services.ClaimNotificationUsers.Queries;

public class ClaimNotificationUserVmQuery : GetByIdQuery, IRequest<ClaimNotificationUserVm>
{
}

public class ClaimNotificationUserVmQueryHandler : BaseQueryHandler, IRequestHandler<ClaimNotificationUserVmQuery, ClaimNotificationUserVm>
{
    public ClaimNotificationUserVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ClaimNotificationUserVm> Handle(ClaimNotificationUserVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimNotificationUsers.Include(e => e.User)
                                                          .Include(e => e.EngageRegion)
                                                          .Include(e=> e.ClaimStatus)
                                                          .SingleAsync(e => e.ClaimNotificationUserId == request.Id, cancellationToken);

        return _mapper.Map<ClaimNotificationUser, ClaimNotificationUserVm>(entity);
    }
}
