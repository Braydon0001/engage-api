// auto-generated
namespace Engage.Application.Services.SupplierAllowances.Queries;

public class SupplierAllowanceListQuery : IRequest<List<SupplierAllowanceDto>>
{

}

public class SupplierAllowanceListHandler : ListQueryHandler, IRequestHandler<SupplierAllowanceListQuery, List<SupplierAllowanceDto>>
{
    public SupplierAllowanceListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierAllowanceDto>> Handle(SupplierAllowanceListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierAllowances.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Vendor)
                              .ThenBy(e => e.SupplierId)
                              .ProjectTo<SupplierAllowanceDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}