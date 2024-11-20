namespace Engage.Application.Services.EngageRegionClaimManagers.Commands;

public class CreateEngageRegionClaimManagersCommand : IRequest<OperationStatus>
{
    public int EngageRegionId { get; set; }

    public List<int> UserIds { get; set; }
}

public class CreateEngageRegionClaimManagersCommandHandler : IRequestHandler<CreateEngageRegionClaimManagersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;


    public CreateEngageRegionClaimManagersCommandHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<OperationStatus> Handle(CreateEngageRegionClaimManagersCommand request, CancellationToken cancellationToken)
    {
        var ids = await _context.EngageRegionClaimManagers.IgnoreQueryFilters()
                                                             .Where(e => e.EngageRegionId == request.EngageRegionId &&
                                                                         request.UserIds.Contains(e.UserId))
                                                             .Select(e => e.UserId)        
                                                             .ToListAsync(cancellationToken);

        var newIds = request.UserIds.Except(ids);

        foreach (var id in newIds)
        {
            _context.EngageRegionClaimManagers.Add(new EngageRegionClaimManager { 
               EngageRegionId = request.EngageRegionId,
               UserId = id
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

