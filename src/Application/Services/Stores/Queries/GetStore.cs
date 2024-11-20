namespace Engage.Application.Services.Stores.Queries;

public class GetStoreQuery : GetByIdQuery, IRequest<StoreDto>
{ }

public class GetStoreQueryHandler : BaseQueryHandler, IRequestHandler<GetStoreQuery, StoreDto>
{
    public GetStoreQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<StoreDto> Handle(GetStoreQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Stores
            .Include(x => x.StoreType)
            .Include(x => x.StoreStoreDepartments)
                .ThenInclude(x => x.StoreDepartment)
            .Include(x => x.StoreConceptLevels)
                .ThenInclude(x => x.StoreConcept)
            .FirstOrDefaultAsync(x => x.StoreId == request.Id, cancellationToken);

        return _mapper.Map<Store, StoreDto>(entity);
    }
}
