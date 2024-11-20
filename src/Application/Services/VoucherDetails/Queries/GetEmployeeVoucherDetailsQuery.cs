using Engage.Application.Services.VoucherDetails.Models;

namespace Engage.Application.Services.VoucherDetails.Queries;

public class GetEmployeeVoucherDetailsQuery : GetQuery, IRequest<ListResult<VoucherDetailDto>>
{
    public int EmployeeId { get; set; }
    public int? VoucherStatusId { get; set; }
}

public class GetEmployeeVoucherDetailsQueryHandler : BaseQueryHandler, IRequestHandler<GetEmployeeVoucherDetailsQuery, ListResult<VoucherDetailDto>>
{
    private readonly IMediator _mediator;

    public GetEmployeeVoucherDetailsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<VoucherDetailDto>> Handle(GetEmployeeVoucherDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.VoucherDetails.AsQueryable()
            .Where(e => e.EmployeeId == request.EmployeeId);
        if (request.VoucherStatusId != null)
        {
            query = query.Where(e => e.VoucherDetailStatusId == (int) request.VoucherStatusId);
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
