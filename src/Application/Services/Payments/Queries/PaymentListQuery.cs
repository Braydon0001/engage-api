namespace Engage.Application.Services.Payments.Queries;

public class PaymentListQuery : GetQuery, IRequest<ListResult<PaymentSubTotalDto>>
{
    public int PaymentBatchId { get; set; }
}

public record PaymentListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentListQuery, ListResult<PaymentSubTotalDto>>
{
    public async Task<ListResult<PaymentSubTotalDto>> Handle(PaymentListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Context.Payments.Where(p => p.PaymentBatchId == query.PaymentBatchId && p.PaymentStatusId != (int)PaymentStatusId.Rejected)
                                             .OrderByDescending(e => e.PaymentId)
                                             .ProjectTo<PaymentSubTotalDto>(Mapper.ConfigurationProvider)
                                             .ToListAsync(cancellationToken);

        return new ListResult<PaymentSubTotalDto>(entities);
    }
}


