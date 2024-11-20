using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Payments.Queries;

public class PaymentPaginatedOptionQuery : PaginatedQuery, IRequest<List<PaymentOption>>
{
}

public record PaymentPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<PaymentPaginatedOptionQuery, List<PaymentOption>>
{
    public async Task<List<PaymentOption>> Handle(PaymentPaginatedOptionQuery query, CancellationToken cancellationToken)
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

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<PaymentOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


