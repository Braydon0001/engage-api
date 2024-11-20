using Engage.Application.Services.VatPeriods.Models;

namespace Engage.Application.Services.VatPeriods.Queries;

public class VatPeriodVmQuery : GetByIdQuery, IRequest<VatPeriodVm>
{ }

public class VatPeriodVmQueryHandler : BaseQueryHandler, IRequestHandler<VatPeriodVmQuery, VatPeriodVm>
{
    public VatPeriodVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<VatPeriodVm> Handle(VatPeriodVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.VatPeriods.Include(e => e.Vat)
                                              .SingleAsync(e => e.VatPeriodId == request.Id, cancellationToken);

        return _mapper.Map<VatPeriod, VatPeriodVm>(entity);
    }
}
