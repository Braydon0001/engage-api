using Engage.Application.Services.Vouchers.Models;

namespace Engage.Application.Services.Vouchers.Queries;

public class VoucherPaginatedQuery : PaginatedQuery, IRequest<ListResult<VoucherSubTotalDto>>
{
}

public record VoucherPaginatedHandler(IAppDbContext Context, IMapper Mapper, IUserService User) : IRequestHandler<VoucherPaginatedQuery, ListResult<VoucherSubTotalDto>>
{
    public async Task<ListResult<VoucherSubTotalDto>> Handle(VoucherPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = VoucherPaginationProps.Create();

        var queryable = Context.Vouchers.AsQueryable().AsNoTracking();

        #region Customer Filte
        if (!User.IsHostSupplier)
        {
            queryable = queryable.Where(e => e.SupplierId == User.SupplierId);
        }

        #endregion

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.VoucherId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<VoucherSubTotalDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
