namespace Engage.Application.Services.Stores.Queries;

public class StoreLegalVmQuery : GetByIdQuery, IRequest<StoreLegalVm>
{
}
public class StoreLegalVmHandler : BaseQueryHandler, IRequestHandler<StoreLegalVmQuery, StoreLegalVm>
{
    public StoreLegalVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreLegalVm> Handle(StoreLegalVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Stores.IgnoreQueryFilters()
                                          .SingleOrDefaultAsync(e => e.StoreId == request.Id, cancellationToken);

        return _mapper.Map<Store, StoreLegalVm>(entity);
    }
}