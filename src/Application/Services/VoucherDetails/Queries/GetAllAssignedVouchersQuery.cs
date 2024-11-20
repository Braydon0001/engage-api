using Engage.Application.Services.VoucherDetails.Models;

namespace Engage.Application.Services.VoucherDetails.Queries;

public class GetAllAssignedVouchersQuery : GetQuery, IRequest<ListResult<VoucherDetailDto>>
{
}

public class GetAllAssignedVouchersQueryHandler : BaseQueryHandler, IRequestHandler<GetAllAssignedVouchersQuery, ListResult<VoucherDetailDto>>
{
    private readonly IMediator _mediator;

    public GetAllAssignedVouchersQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<VoucherDetailDto>> Handle(GetAllAssignedVouchersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.VoucherDetails.Where(e => e.VoucherDetailStatusId == (int)VoucherDetailStatusId.Assigned)
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
