using Engage.Application.Services.CreditorNotificationStatusUsers.Models;

namespace Engage.Application.Services.CreditorNotificationStatusUsers.Queries;

public class CreditorNotificationStatusUserVmQuery : GetByIdQuery, IRequest<CreditorNotificationStatusUserVm>
{
}

public class CreditorNotificationStatusUserVmQueryHandler : BaseQueryHandler, IRequestHandler<CreditorNotificationStatusUserVmQuery, CreditorNotificationStatusUserVm>
{
    public CreditorNotificationStatusUserVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<CreditorNotificationStatusUserVm> Handle(CreditorNotificationStatusUserVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CreditorNotificationStatusUsers.Include(e => e.User)
                                                          .Include(e => e.EngageRegion)
                                                          .Include(e => e.CreditorStatus)
                                                          .SingleAsync(e => e.CreditorNotificationStatusUserId == request.Id, cancellationToken);

        return _mapper.Map<CreditorNotificationStatusUser, CreditorNotificationStatusUserVm>(entity);
    }
}
