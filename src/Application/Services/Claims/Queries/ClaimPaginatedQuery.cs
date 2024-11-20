using Engage.Application.Services.Claims.Models;

namespace Engage.Application.Services.Claims.Queries;

public class ClaimPaginatedQuery : PaginatedQuery, IRequest<ListResult<ClaimSubTotalDto>>
{
}

public record ClaimPaginatedHandler(IAppDbContext Context, IMapper Mapper, IUserService User) : IRequestHandler<ClaimPaginatedQuery, ListResult<ClaimSubTotalDto>>
{
    public async Task<ListResult<ClaimSubTotalDto>> Handle(ClaimPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ClaimPaginationProps.Create();

        var queryable = Context.Claims.AsQueryable().AsNoTracking();

        #region Custom Filters
        if (!User.IsHostSupplier)
        {
            var userId = await User.GetUserIdAsync();

            queryable = queryable.Where(e => e.SupplierId == User.SupplierId &&
                                             e.ClaimAccountManager.UserId == userId);
        }

        #endregion

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ClaimId);
        }

        var entities = await queryable.Paginate(query, props)
                                      .ProjectTo<ClaimSubTotalDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
