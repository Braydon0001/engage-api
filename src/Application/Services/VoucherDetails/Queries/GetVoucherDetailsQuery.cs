using Engage.Application.Services.VoucherDetails.Models;

namespace Engage.Application.Services.VoucherDetails.Queries;

public class GetVoucherDetailsQuery : GetQuery, IRequest<ListResult<VoucherDetailDto>>
{
    public int VoucherId { get; set; }
}

public class GetVoucherDetailsQueryHandler : BaseQueryHandler, IRequestHandler<GetVoucherDetailsQuery, ListResult<VoucherDetailDto>>
{
    private readonly IMediator _mediator;

    public GetVoucherDetailsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<VoucherDetailDto>> Handle(GetVoucherDetailsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.VoucherDetails.Where(e => e.VoucherId == request.VoucherId)
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
