// auto-generated
namespace Engage.Application.Services.SupplierSubContractDetails.Queries;

public class SupplierSubContractDetailListQuery : IRequest<List<SupplierSubContractDetailDto>>
{

}

public class SupplierSubContractDetailListHandler : ListQueryHandler, IRequestHandler<SupplierSubContractDetailListQuery, List<SupplierSubContractDetailDto>>
{
    public SupplierSubContractDetailListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractDetailDto>> Handle(SupplierSubContractDetailListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractDetails.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierSubContractDetailId)
                              .ProjectTo<SupplierSubContractDetailDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}