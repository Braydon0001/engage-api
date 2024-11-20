// auto-generated
namespace Engage.Application.Services.SupplierContractSplits.Queries;

public class SupplierContractSplitListQuery : IRequest<List<SupplierContractSplitDto>>
{

}

public class SupplierContractSplitListHandler : ListQueryHandler, IRequestHandler<SupplierContractSplitListQuery, List<SupplierContractSplitDto>>
{
    public SupplierContractSplitListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractSplitDto>> Handle(SupplierContractSplitListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractSplits.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractSplitId)
                              .ProjectTo<SupplierContractSplitDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}