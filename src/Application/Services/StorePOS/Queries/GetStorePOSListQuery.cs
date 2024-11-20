using Engage.Application.Services.StorePOS.Models;

namespace Engage.Application.Services.StorePOS.Queries;

public class GetStorePOSListQuery : GetQuery, IRequest<ListResult<StorePOSDto>>
{
    public int StoreId { get; set; }
}

public class GetStorePOSListQueryHandler : BaseQueryHandler, IRequestHandler<GetStorePOSListQuery, ListResult<StorePOSDto>>
{
    public GetStorePOSListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StorePOSDto>> Handle(GetStorePOSListQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.StorePOS.Where(e => e.StoreId == request.StoreId)
                                              .OrderBy(e => e.StorePOSId)
                                              .ProjectTo<StorePOSDto>(_mapper.ConfigurationProvider)
                                              .ToListAsync(cancellationToken);

        return new ListResult<StorePOSDto>
        {
            Count = entities.Count,
            Data = entities
        };
    }
}
