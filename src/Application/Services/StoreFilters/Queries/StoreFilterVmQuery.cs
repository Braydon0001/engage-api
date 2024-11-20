using Engage.Application.Services.StoreFilters.Models;

namespace Engage.Application.Services.StoreFilters.Queries;

public class StoreFilterVmQuery : IRequest<StoreFilterVm>
{
    public int Id { get; set; }
}

public class StoreFilterVmQueryHandler : BaseQueryHandler, IRequestHandler<StoreFilterVmQuery, StoreFilterVm>
{
    public StoreFilterVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreFilterVm> Handle(StoreFilterVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreFilters.Include(e => e.Store)
                                                .Include(e => e.FileUpload)
                                                .SingleAsync(e => e.StoreFilterId == request.Id, cancellationToken);

        return _mapper.Map<StoreFilter, StoreFilterVm>(entity);
    }
}
