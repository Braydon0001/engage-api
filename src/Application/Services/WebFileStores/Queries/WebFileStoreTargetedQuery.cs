using Engage.Application.Services.WebFileCategories.Queries;
using Engage.Application.Services.WebFiles.Queries;

namespace Engage.Application.Services.WebFileStores.Queries;

public record WebFileStoreTargeted(IEnumerable<WebFileCategoryDto> Categories, IEnumerable<WebFileDto> Files)
{
}

public record WebFileStoreTargetedQuery(int StoreId, DateTime Date) : IRequest<WebFileStoreTargeted>
{
}

public class WebFileStoreTargetedHandler : IRequestHandler<WebFileStoreTargetedQuery, WebFileStoreTargeted>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public WebFileStoreTargetedHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WebFileStoreTargeted> Handle(WebFileStoreTargetedQuery query, CancellationToken cancellationToken)
    {
        var store = await _context.Stores.Include(e => e.StoreFormat)
                                         .SingleOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken);
        if (store == null)
        {
            return null;
        }

        var categories = await _context.WebFileCategories.Where(e => e.WebFileGroupId == (int)WebFileGroupEnum.Store)
                                                         .ProjectTo<WebFileCategoryDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);
        var categoryIds = categories.Select(e => e.Id).ToList();

        var storeWebFileIds = await _context.WebFileStores.Where(e => e.StoreId == query.StoreId && query.Date >= e.WebFile.StartDate && (!e.WebFile.EndDate.HasValue || query.Date <= e.WebFile.EndDate))
                                                          .Select(e => e.WebFileId)
                                                          .ToListAsync(cancellationToken);

        var storeFormatWebFileIds = await _context.WebFileStoreFormats.Where(e => e.StoreFormatId == store.StoreFormatId && store.StoreFormat.Disabled == false)
                                                                      .Select(e => e.WebFileId)
                                                                      .ToListAsync(cancellationToken);

        var webFileIds = storeWebFileIds.Union(storeFormatWebFileIds).Distinct().ToList();

        var webFiles = await _context.WebFiles.Where(e => e.Disabled == false &&
                                                          query.Date >= e.StartDate && (!e.EndDate.HasValue || query.Date <= e.EndDate) &&
                                                          categoryIds.Contains(e.WebFileCategoryId) &&
                                                          ((e.TargetStrategyId != (int)TargetStrategyEnum.All && webFileIds.Contains(e.WebFileId)) ||
                                                            e.TargetStrategyId == (int)TargetStrategyEnum.All ||
                                                            (e.NPrintingId != null && e.StoreId == query.StoreId)))
                                              .ProjectTo<WebFileDto>(_mapper.ConfigurationProvider)
                                              .ToListAsync(cancellationToken);

        var files = webFiles.Select(e =>
                               {
                                   if (e.Files != null && e.Files.Count == 1)
                                   {
                                       var jsonFile = e.Files[0];
                                       e.FileUrl = jsonFile.Url;
                                       e.FileName = jsonFile.Name;
                                   }
                                   return e;
                               })
                               .OrderByDescending(e => e.Created)
                               .ToList();

        return new WebFileStoreTargeted(categories, files);
    }
}
