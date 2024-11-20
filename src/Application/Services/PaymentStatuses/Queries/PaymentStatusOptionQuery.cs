namespace Engage.Application.Services.PaymentStatuses.Queries;

public class PaymentStatusOptionQuery : IRequest<List<PaymentStatusOption>>
{
    public bool IsProcessable { get; set; }
}

public record PaymentStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusOptionQuery, List<PaymentStatusOption>>
{
    public async Task<List<PaymentStatusOption>> Handle(PaymentStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.PaymentStatuses.AsQueryable().AsNoTracking();

        if (query.IsProcessable)
        {
            queryable = queryable.Where(e => e.PaymentStatusId != (int)PaymentStatusId.Archived && e.PaymentStatusId != (int)PaymentStatusId.Rejected);
        }

        return await queryable.OrderBy(e => e.PaymentStatusId)
                              .ProjectTo<PaymentStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}