namespace Engage.Application.Services.SupplierYears.Queries;

public class SupplierYearOptionListQuery : IRequest<List<SupplierYearOption>>
{
}

public class SupplierYearOptionListHandler : ListQueryHandler, IRequestHandler<SupplierYearOptionListQuery, List<SupplierYearOption>>
{
    public SupplierYearOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierYearOption>> Handle(SupplierYearOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierYears.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
            .ProjectTo<SupplierYearOption>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
