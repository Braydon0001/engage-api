namespace Engage.Application.Services.SupplierPeriods.Queries;

public class SupplierPeriodOptionListQuery : IRequest<List<SupplierPeriodOption>>
{
}

public class SupplierPeriodOptionListHandler : ListQueryHandler, IRequestHandler<SupplierPeriodOptionListQuery, List<SupplierPeriodOption>>
{
    public SupplierPeriodOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierPeriodOption>> Handle(SupplierPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierPeriods.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.SupplierYearId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<SupplierPeriodOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
