using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.CategoryFileStores;

// Queries
public class CategoryFileStoresQuery : GetQuery, IRequest<ListResult<StoreListDto>>
{
    public int CategoryFileId { get; set; }
}

// Handlers
public class CategoryFileStoresQueryHandler : BaseQueryHandler, IRequestHandler<CategoryFileStoresQuery, ListResult<StoreListDto>>
{

    public CategoryFileStoresQueryHandler(IAppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<ListResult<StoreListDto>> Handle(CategoryFileStoresQuery query, CancellationToken cancellationToken)
    {
        var CategoryFile = await _context.CategoryFiles.Where(e => e.CategoryFileId == query.CategoryFileId)
                                           .FirstOrDefaultAsync(cancellationToken);
        if (CategoryFile == null)
        {
            throw new NotFoundException(nameof(CategoryFile), query.CategoryFileId);
        }

        var storeIds = await _context.CategoryFileStores.Where(e => e.CategoryFileId == query.CategoryFileId)
                                                  .Select(e => e.StoreId)
                                                  .ToListAsync(cancellationToken);

        var entities = new List<StoreListDto>();
        if (storeIds.Count > 0)
        {
            entities = await _context.Stores.Where(e => storeIds.Contains(e.StoreId))
                                            .ProjectTo<StoreListDto>(_mapper.ConfigurationProvider)
                                            .OrderBy(e => e.Code)
                                            .ToListAsync(cancellationToken);
        }

        return new ListResult<StoreListDto>
        {
            Count = entities.Count,
            Data = entities
        };
    }
}
