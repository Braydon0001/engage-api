namespace Engage.Application.Services.WebFileStores.Queries;

public class StoreTarget
{
    public StoreTarget(int id, string code, string name)
    {
        Id = id;
        Code = code;
        Name = name;
        Discriminator = "WebFileStore";
    }



    public int Id { get; }
    public string Code { get; }
    public string Name { get; }
    public string Discriminator { get; }
}

public record WebFileStoreTargetListQuery(int WebFileId) : IRequest<ListResult<StoreTarget>>
{
}

public class WebFileStoreTargetListHandler : IRequestHandler<WebFileStoreTargetListQuery, ListResult<StoreTarget>>
{
    private readonly IAppDbContext _context;

    public WebFileStoreTargetListHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ListResult<StoreTarget>> Handle(WebFileStoreTargetListQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == query.WebFileId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        // Get the Stores 
        var stores = await _context.WebFileStores.IgnoreQueryFilters()
                                                 .Where(e => e.WebFileId == query.WebFileId)
                                                 .Select(e => new StoreTarget(e.Store.StoreId, e.Store.Code, e.Store.Name))
                                                 .ToListAsync(cancellationToken);

        return new ListResult<StoreTarget>(stores);
    }
}
