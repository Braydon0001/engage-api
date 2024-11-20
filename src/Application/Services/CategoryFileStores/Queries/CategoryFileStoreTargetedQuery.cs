using Engage.Application.Services.CategoryFiles.Queries;

namespace Engage.Application.Services.CategoryFileStores.Queries;

public record CategoryFileStoreTargetedQuery(int StoreId, DateTime Date) : IRequest<ListResult<CategoryFileDto>>
{
}

public class CategoryFileStoreTargetedHandler : IRequestHandler<CategoryFileStoreTargetedQuery, ListResult<CategoryFileDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryFileStoreTargetedHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListResult<CategoryFileDto>> Handle(CategoryFileStoreTargetedQuery query, CancellationToken cancellationToken)
    {

        var store = await _context.Stores.SingleOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken);

        if (store == null)
        {
            return null;
        }

        var targetedCategoryFiles = await _context.CategoryFileTargets.Select(e => e.CategoryFileId).ToListAsync(cancellationToken);

        var storeCategoryFileIds = await _context.CategoryFileStores.Where(e => e.StoreId == query.StoreId && query.Date.Date >= e.CategoryFile.StartDate.Date && (!e.CategoryFile.EndDate.HasValue || query.Date.Date <= e.CategoryFile.EndDate.Value.Date))
                                                                          .Select(e => e.CategoryFileId)
                                                                          .ToListAsync(cancellationToken);

        var storeFormatIds = _context.Stores.Where(e => e.StoreId == query.StoreId).Select(e => e.StoreFormatId).ToList();
        var formatCategoryFileIds = await _context.CategoryFileStoreFormats.Where(e => storeFormatIds.Contains(e.StoreFormatId))
                                                                           .Select(e => e.CategoryFileId)
                                                                           .ToListAsync(cancellationToken);

        var categoryGroupIds = _context.CategoryStoreGroups.Where(e => e.StoreId == query.StoreId).Select(e => e.CategoryGroupId).ToList();
        var categoryFileCategoryGroupIds = await _context.CategoryFileCategoryGroups.Where(e => categoryGroupIds.Contains(e.CategoryGroupId))
                                                                               .Select(e => e.CategoryFileId)
                                                                               .ToListAsync(cancellationToken);

        var CategoryFileIds = storeCategoryFileIds.Union(formatCategoryFileIds).Union(categoryFileCategoryGroupIds).Distinct().ToList();

        var CategoryFiles = await _context.CategoryFiles.Where(e => e.Disabled == false &&
                                                                    query.Date.Date >= e.StartDate.Date && (!e.EndDate.HasValue
                                                                    || query.Date.Date <= e.EndDate.Value.Date) &&
                                                                    ((targetedCategoryFiles.Contains(e.CategoryFileId)
                                                                    && CategoryFileIds.Contains(e.CategoryFileId))
                                                                    //|| !targetedCategoryFiles.Contains(e.CategoryFileId)
                                                                    ))
                                                        .ProjectTo<CategoryFileDto>(_mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);

        return new ListResult<CategoryFileDto>(CategoryFiles);
    }
}