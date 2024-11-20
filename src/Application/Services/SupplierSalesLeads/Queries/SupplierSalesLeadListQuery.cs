// auto-generated
namespace Engage.Application.Services.SupplierSalesLeads.Queries;

public class SupplierSalesLeadListQuery : IRequest<List<SupplierSalesLeadDto>>
{

}

public class SupplierSalesLeadListHandler : ListQueryHandler, IRequestHandler<SupplierSalesLeadListQuery, List<SupplierSalesLeadDto>>
{
    public SupplierSalesLeadListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSalesLeadDto>> Handle(SupplierSalesLeadListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSalesLeads.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.FirstName)
                              .ThenBy(e => e.LastName)
                              .ThenBy(e => e.KnownAs)
                              .ProjectTo<SupplierSalesLeadDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}