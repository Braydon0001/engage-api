namespace Engage.Application.Services.DCProducts.Queries;

public class DCProductOptionsOfflineQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int UserId { get; set; }
}

public class DCProductOptionsOfflineQueryHandler : BaseQueryHandler, IRequestHandler<DCProductOptionsOfflineQuery, List<OptionDto>>
{
    private readonly IUserService _user;

    public DCProductOptionsOfflineQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(DCProductOptionsOfflineQuery query, CancellationToken cancellationToken)
    {


        var userStakeholderIds = await _context.ProjectStakeholderUsers.Where(e => e.UserId == query.UserId && e.Disabled != true).Select(e => e.ProjectStakeholderId).ToListAsync(cancellationToken);





        var projectIds = await _context.ProjectAssignees.AsNoTracking()
                                                       .Where(e => userStakeholderIds.Contains(e.ProjectStakeholderId))
                                                       .Include(e => e.ProjectStakeholder)
                                                       .Select(e => e.ProjectId)
                                                       .ToListAsync(cancellationToken);

        var ownerProjectIds = await _context.ProjectStores.Where(e => e.OwnerId == query.UserId).Select(e => e.ProjectId).ToListAsync(cancellationToken);


        projectIds.AddRange(ownerProjectIds);


        var projectDcProductsId = await _context.ProjectDcProducts.Where(e => projectIds.Contains(e.ProjectId)).Select(e => e.DcProductId).ToListAsync(cancellationToken);


        var queryable = _context.DCProducts.Where(e => projectDcProductsId.Contains(e.DCProductId) && e.Disabled == false && e.Deleted == false
                                              && e.EngageVariantProduct.Disabled == false
                                              && e.EngageVariantProduct.Deleted == false
                                              && e.EngageVariantProduct.EngageMasterProduct.Disabled == false
                                              && e.EngageVariantProduct.EngageMasterProduct.Deleted == false)
                                      .AsQueryable();



        return await queryable
                     .Where(e => (
                                 EF.Functions.Like(e.Code, $"%{query.Search}%") ||
                                 EF.Functions.Like(e.Name, $"%{query.Search}%") ||
                                 EF.Functions.Like(e.Code, $"%{query.Search}%") ||
                                 EF.Functions.Like(e.Name, $"%{query.Search}%")))
                     .Select(e => new OptionDto
                     {
                         Id = e.DCProductId,
                         Name = e.Name
                     })
                     .OrderBy(e => e.Name)
                     .ToListAsync(cancellationToken);
    }
}
