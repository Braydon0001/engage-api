namespace Engage.Application.Services.SupplierPeriods.Queries;

public class SupplierPeriodListQuery : IRequest<List<SupplierPeriodDto>>
{
    public int? SupplierYearId { get; set; }
}

public class SupplierPeriodListHandler : ListQueryHandler, IRequestHandler<SupplierPeriodListQuery, List<SupplierPeriodDto>>
{
    public SupplierPeriodListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierPeriodDto>> Handle(SupplierPeriodListQuery query, CancellationToken cancellationToken)
    {


        var queryable = _context.SupplierPeriods.AsQueryable().AsNoTracking();

        if (query.SupplierYearId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierYearId == query.SupplierYearId);
        }


        return await queryable.OrderBy(e => e.SupplierYearId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<SupplierPeriodDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
