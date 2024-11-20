namespace Engage.Application.Services.StoreCycles.Queries;

public class StoreCycleListQuery : IRequest<List<StoreCycleDto>>
{

}

public class StoreCycleListHandler : ListQueryHandler, IRequestHandler<StoreCycleListQuery, List<StoreCycleDto>>
{
    public StoreCycleListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreCycleDto>> Handle(StoreCycleListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreCycles.AsQueryable().AsNoTracking();

        return await queryable.OrderByDescending(e => e.StoreCycleId)
                              .ProjectTo<StoreCycleDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}