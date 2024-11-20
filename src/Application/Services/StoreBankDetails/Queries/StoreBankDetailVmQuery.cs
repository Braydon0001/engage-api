using Engage.Application.Services.StoreBankDetails.Models;

namespace Engage.Application.Services.StoreBankDetails.Queries;

public class StoreBankDetailVmQuery :  IRequest<StoreBankDetailVm>
{
    public int Id { get; set; }
}

public class StoreBankDetailVmQueryHandler : BaseQueryHandler, IRequestHandler<StoreBankDetailVmQuery, StoreBankDetailVm>
{
    public StoreBankDetailVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreBankDetailVm> Handle(StoreBankDetailVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreBankDetails.Include(e => e.Store)
                                                    .SingleAsync(e => e.StoreBankDetailId == request.Id, cancellationToken);

        return _mapper.Map<StoreBankDetail, StoreBankDetailVm>(entity);
    }
}
