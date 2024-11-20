using Engage.Application.Services.VoucherDetails.Models;

namespace Engage.Application.Services.VoucherDetails.Queries;

public class GetAllAvailableVouchersQuery : GetQuery, IRequest<ListResult<VoucherDetailDto>>
{
    public int? EngageRegionId { get; set; }
}

public class GetAllAvailableVouchersQueryHandler : BaseQueryHandler, IRequestHandler<GetAllAvailableVouchersQuery, ListResult<VoucherDetailDto>>
{
    private readonly IMediator _mediator;

    public GetAllAvailableVouchersQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<VoucherDetailDto>> Handle(GetAllAvailableVouchersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.VoucherDetails.Where(e => e.VoucherDetailStatusId != (int)VoucherDetailStatusId.Issued).AsQueryable();

        if (request.EngageRegionId.HasValue)
        {
            query = query
                        .Where(e => e.Voucher.EngageRegionId == request.EngageRegionId.Value);

        }

        var entities = await query
                                .OrderBy(e => e.VoucherDetailId)
                                .ProjectTo<VoucherDetailDto>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

        return new ListResult<VoucherDetailDto>
        {
            Count = entities.Count,
            Data = entities,
        };
    }


}
