namespace Engage.Application.Services.SupplierYears.Queries;

public class SupplierYearListQuery : IRequest<List<SupplierYearDto>>
{

}

public class SupplierYearListHandler : ListQueryHandler, IRequestHandler<SupplierYearListQuery, List<SupplierYearDto>>
{
    public SupplierYearListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierYearDto>> Handle(SupplierYearListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierYears.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
            .ProjectTo<SupplierYearDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
