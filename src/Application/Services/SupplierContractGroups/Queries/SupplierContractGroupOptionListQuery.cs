// auto-generated
namespace Engage.Application.Services.SupplierContractGroups.Queries;

public class SupplierContractGroupOptionListQuery : IRequest<List<SupplierContractGroupOption>>
{ 

}

public class SupplierContractGroupOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractGroupOptionListQuery, List<SupplierContractGroupOption>>
{
    public SupplierContractGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractGroupOption>> Handle(SupplierContractGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractGroupId)
                              .ProjectTo<SupplierContractGroupOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}