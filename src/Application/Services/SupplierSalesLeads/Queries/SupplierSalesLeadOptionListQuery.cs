// auto-generated
namespace Engage.Application.Services.SupplierSalesLeads.Queries;

public class SupplierSalesLeadOptionListQuery : IRequest<List<SupplierSalesLeadOption>>
{ 

}

public class SupplierSalesLeadOptionListHandler : ListQueryHandler, IRequestHandler<SupplierSalesLeadOptionListQuery, List<SupplierSalesLeadOption>>
{
    public SupplierSalesLeadOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSalesLeadOption>> Handle(SupplierSalesLeadOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSalesLeads.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.FirstName)
                              .ThenBy(e => e.LastName)
                              .ThenBy(e => e.KnownAs)
                              .ProjectTo<SupplierSalesLeadOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}