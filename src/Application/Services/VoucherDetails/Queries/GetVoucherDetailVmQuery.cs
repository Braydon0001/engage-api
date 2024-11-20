using Engage.Application.Services.VoucherDetails.Models;

namespace Engage.Application.Services.VoucherDetails.Queries;

public class GetVoucherDetailVmQuery : GetByIdQuery, IRequest<VoucherDetailVm>
{
}

public class GetVoucherDetailVmQueryHandler : BaseQueryHandler, IRequestHandler<GetVoucherDetailVmQuery, VoucherDetailVm>
{
    public GetVoucherDetailVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<VoucherDetailVm> Handle(GetVoucherDetailVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.VoucherDetails.Include(e => e.VoucherDetailStatus)
                                             .Include(e => e.Employee)
                                             .Include(e => e.Store)
                                             .Include(e => e.Claim)
                                                .ThenInclude(c => c.Store)
                                             .SingleAsync(e => e.VoucherDetailId == request.Id, cancellationToken);
        return _mapper.Map<VoucherDetail, VoucherDetailVm>(entity);
    }
}
