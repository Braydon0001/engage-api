using Engage.Application.Services.Vouchers.Models;

namespace Engage.Application.Services.Vouchers.Queries;

public record VoucherVmQuery(int Id) : IRequest<VoucherVm>
{
}

public record VoucherVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<VoucherVmQuery, VoucherVm>
{


    public async Task<VoucherVm> Handle(VoucherVmQuery request, CancellationToken cancellationToken)
    {
        var Voucher = await Context.Vouchers.IgnoreQueryFilters()
                                            .Include(e => e.Supplier)
                                            .Include(e => e.VoucherStatus)
                                            .Include(e => e.EngageRegion)
                                            .SingleAsync(x => x.VoucherId == request.Id, cancellationToken);

        return Mapper.Map<Voucher, VoucherVm>(Voucher);
    }
}
