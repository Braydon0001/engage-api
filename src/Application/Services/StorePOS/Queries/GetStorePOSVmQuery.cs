using Engage.Application.Services.StorePOS.Models;

namespace Engage.Application.Services.StorePOS.Queries;

public class GetStorePOSVmQuery : GetByIdQuery, IRequest<StorePOSVm>
{

}

public class GetStorePOSVmQueryHandler : BaseListQueryHandler, IRequestHandler<GetStorePOSVmQuery, StorePOSVm>
{
    public GetStorePOSVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StorePOSVm> Handle(GetStorePOSVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StorePOS.Include(e => e.Store)
                                            .Include(e => e.StorePOSType)
                                            .SingleAsync(e => e.StorePOSId == request.Id, cancellationToken);

        return _mapper.Map<Domain.Entities.StorePOS, StorePOSVm>(entity);
    }
}
