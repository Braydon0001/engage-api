using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.Stores.Queries;

public class StoresQuery : GetQuery, IRequest<ListResult<StoreListDto>>
{
    public int? ParentStoreId { get; set; }
}

public class StoresQueryHandler : BaseQueryHandler, IRequestHandler<StoresQuery, ListResult<StoreListDto>>
{
    public StoresQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreListDto>> Handle(StoresQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Stores.AsQueryable();

        if (request.ParentStoreId.HasValue)
        {
            queryable = queryable.Where(e => e.ParentStoreId == request.ParentStoreId.Value);
        }

        var entities = await queryable.OrderBy(e => e.Name)
                                      .ProjectTo<StoreListDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<StoreListDto>(entities);
    }
}
