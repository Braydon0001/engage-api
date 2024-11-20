using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Payments.Queries;

public class GetProcessPaymentsListQuery : IRequest<ListResult<PaymentSubTotalDto>>
{
    public int? EngageRegionId { get; set; }
    public int? PaymentStatusId { get; set; }
    public List<int> PaymentStatusIds { get; set; }
}

public class GetProcessPaymentsListQueryHandler : BaseQueryHandler, IRequestHandler<GetProcessPaymentsListQuery, ListResult<PaymentSubTotalDto>>
{
    private readonly IMediator _mediator;
    public GetProcessPaymentsListQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<PaymentSubTotalDto>> Handle(GetProcessPaymentsListQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Payments.AsQueryable();

        if (request.EngageRegionId.HasValue)
        {
            var batchIds = await _context.PaymentBatchRegions.Where(e => e.EngageRegionId == request.EngageRegionId.Value)
                                                             .Select(e => e.PaymentBatchId)
                                                             .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => batchIds.Contains(e.PaymentBatchId));
        }

        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        if (!engageRegionIds.Contains(7))
        {
            var batchIds = await _context.PaymentBatchRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId))
                                                            .Select(e => e.PaymentBatchId)
                                                            .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => batchIds.Contains(e.PaymentBatchId));
        }

        if (request.PaymentStatusIds != null && request.PaymentStatusIds.Count > 0)
        {
            queryable = queryable.Where(e => request.PaymentStatusIds.Contains(e.PaymentStatusId));
        }

        if (request.PaymentStatusId.HasValue)
        {
            queryable = queryable.Where(e => e.PaymentStatusId == request.PaymentStatusId.Value);
        }

        var entities = await queryable.OrderByDescending(e => e.PaymentId)
                                      .ProjectTo<PaymentSubTotalDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<PaymentSubTotalDto>(entities);
    }
}
