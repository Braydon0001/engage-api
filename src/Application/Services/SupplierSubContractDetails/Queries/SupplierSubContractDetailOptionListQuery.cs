// auto-generated
namespace Engage.Application.Services.SupplierSubContractDetails.Queries;

public class SupplierSubContractDetailOptionListQuery : IRequest<List<SupplierSubContractDetailOption>>
{ 

}

public class SupplierSubContractDetailOptionListHandler : ListQueryHandler, IRequestHandler<SupplierSubContractDetailOptionListQuery, List<SupplierSubContractDetailOption>>
{
    public SupplierSubContractDetailOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractDetailOption>> Handle(SupplierSubContractDetailOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractDetails.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierSubContractDetailId)
                              .ProjectTo<SupplierSubContractDetailOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}