using Engage.Application.Services.ClaimFloats.Models;

namespace Engage.Application.Services.ClaimFloats.Queries;

public class ClaimFloatPaginatedQuery : PaginatedQuery, IRequest<ListResult<ClaimFloatDto>>
{
}

public record ClaimFloatPaginatedHandler(IAppDbContext Context, IMapper Mapper, IUserService User) : IRequestHandler<ClaimFloatPaginatedQuery, ListResult<ClaimFloatDto>>
{
    public async Task<ListResult<ClaimFloatDto>> Handle(ClaimFloatPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ClaimFloatPaginationProps.Create();

        var queryable = Context.ClaimFloats.AsQueryable().AsNoTracking();

        #region Customer Filter
        if (!User.IsHostSupplier)
        {
            queryable = queryable.Where(e => e.SupplierId == User.SupplierId);
        }

        #endregion

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ClaimFloatId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<ClaimFloatDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
