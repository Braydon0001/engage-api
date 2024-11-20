using Engage.Application.Services.DCAccounts.Models;

namespace Engage.Application.Services.DCAccounts.Queries;

public class DCAccountsQuery : GetQuery, IRequest<ListResult<DCAccountDto>>
{
    public int? StoreId { get; set; }
}

public class DCAccountsQueryHandler : BaseQueryHandler, IRequestHandler<DCAccountsQuery, ListResult<DCAccountDto>>
{
    public DCAccountsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<DCAccountDto>> Handle(DCAccountsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.DCAccounts.AsQueryable();

        if (request.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == request.StoreId);
        }

        var entities = await queryable.OrderBy(e => e.AccountNumber)
                                      .ProjectTo<DCAccountDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<DCAccountDto>(entities);
    }
}
