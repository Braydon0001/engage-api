using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.PaymentBatches.Queries;

public class PaymentBatchPaginatedQuery : PaginatedQuery, IRequest<List<PaymentBatchDto>>
{
}

public record PaymentBatchPaginatedHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<PaymentBatchPaginatedQuery, List<PaymentBatchDto>>
{
    public async Task<List<PaymentBatchDto>> Handle(PaymentBatchPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = PaymentBatchPaginationProps.Create();

        var queryable = Context.PaymentBatches.AsQueryable().AsNoTracking();

        var engageRegionIds = await Mediator.Send(new UserRegionsQuery(), cancellationToken);

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(Context.PaymentBatchRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      batch => batch.PaymentBatchId,
                                      region => region.PaymentBatchId,
                                      (batch, region) => batch).Distinct();
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
            queryable = queryable.OrderByDescending(e => e.PaymentBatchId);
        }

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<PaymentBatchDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


