// auto-generated
namespace Engage.Application.Services.SupplierContractSplits.Queries;

public class SupplierContractSplitOptionListQuery : IRequest<List<SupplierContractSplitOption>>
{ 

}

public class SupplierContractSplitOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractSplitOptionListQuery, List<SupplierContractSplitOption>>
{
    public SupplierContractSplitOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractSplitOption>> Handle(SupplierContractSplitOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractSplits.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractSplitId)
                              .ProjectTo<SupplierContractSplitOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}