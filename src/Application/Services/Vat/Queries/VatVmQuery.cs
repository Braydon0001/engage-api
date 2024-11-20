using Engage.Application.Services.Vat.Models;

namespace Engage.Application.Services.Vat.Queries;

public class VatVmQuery : GetByIdQuery, IRequest<VatVm>
{
}

public class VatVMQueryHandler : BaseQueryHandler, IRequestHandler<VatVmQuery, VatVm>
{
    public VatVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<VatVm> Handle(VatVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vat.SingleAsync(x => x.VatId == request.Id, cancellationToken);
        return _mapper.Map<Domain.Entities.Vat, VatVm>(entity);
    }
}
