using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Payments.Queries;

public class PaymentPaginatedQuery : PaginatedQuery, IRequest<List<PaymentSubTotalDto>>
{
}

public record PaymentPaginatedHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<PaymentPaginatedQuery, List<PaymentSubTotalDto>>
{
    public async Task<List<PaymentSubTotalDto>> Handle(PaymentPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = PaymentPaginationProps.Create();

        var queryable = Context.Payments.AsQueryable().AsNoTracking();

        var engageRegionIds = await Mediator.Send(new UserRegionsQuery(), cancellationToken);
        if (!engageRegionIds.Contains(7))
        {
            var batchIds = await Context.PaymentBatchRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId))
                                                            .Select(e => e.PaymentBatchId)
                                                            .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => batchIds.Contains(e.PaymentBatchId));
        }

        #region Custom Filters
        if (query.FilterModel != null)
        {
            query.FilterModel.TryGetValue("engageRegions", out FilterModel engageRegions);
            if (engageRegions != null && engageRegions.Values.Count > 0)
            {
                var batchIds = await Context.PaymentBatchRegions.Where(e => engageRegions.Values.Contains(e.EngageRegionId))
                                                                .Select(e => e.PaymentBatchId)
                                                                .ToListAsync(cancellationToken);

                queryable = queryable.Where(e => batchIds.Contains(e.PaymentBatchId));
            }
        }
        #endregion

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.PaymentId);
        }

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<PaymentSubTotalDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


