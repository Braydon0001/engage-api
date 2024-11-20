// auto-generated
namespace Engage.Application.Services.SupplierSubContracts.Queries;

public class SupplierSubContractListQuery : IRequest<List<SupplierSubContractDto>>
{

}

public class SupplierSubContractListHandler : ListQueryHandler, IRequestHandler<SupplierSubContractListQuery, List<SupplierSubContractDto>>
{
    public SupplierSubContractListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractDto>> Handle(SupplierSubContractListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContracts.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierSubContractId)
                              .ProjectTo<SupplierSubContractDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}