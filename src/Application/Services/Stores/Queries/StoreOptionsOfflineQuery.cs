namespace Engage.Application.Services.Stores.Queries;

public class StoreOptionsOfflineQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? UserId { get; set; }
}

public class StoreOptionsOfflineQueryHandler : IRequestHandler<StoreOptionsOfflineQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public StoreOptionsOfflineQueryHandler(IAppDbContext context, IMediator mediator, IUserService userService)
    {
        _context = context;
        _mediator = mediator;
        _userService = userService;
    }

    public async Task<List<OptionDto>> Handle(StoreOptionsOfflineQuery query, CancellationToken cancellationToken)
    {
        var userStakeholderIds = await _context.ProjectStakeholderUsers.Where(e => e.UserId == query.UserId && e.Disabled != true)
            .Select(e => e.ProjectStakeholderId)
            .ToListAsync(cancellationToken);





        var projectIds = await _context.ProjectAssignees.AsNoTracking()
                                                       .Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                       .Include(e => e.ProjectStakeholder)
                                                       .Select(e => e.ProjectId)
                                                       .ToListAsync(cancellationToken);

        var ownerProjectIds = await _context.ProjectStores.Where(e => e.OwnerId == query.UserId)
                                                       .Select(e => e.ProjectId)
                                                       .ToListAsync(cancellationToken);


        projectIds.AddRange(ownerProjectIds);

        var userStoreIds = await _context.ProjectStores.Where(e => projectIds.Contains(e.ProjectId)).Select(x => x.StoreId).ToListAsync(cancellationToken);

        var queryable = _context.Stores.Where(e => userStoreIds.Contains(e.StoreId)).AsQueryable();





        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.StoreId, Name = e.Name + " - " + e.EngageRegion.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}

