// auto-generated
namespace Engage.Application.Services.SupplierContracts.Queries;

public class SupplierContractListQuery : IRequest<List<SupplierContractDto>>
{

}

public class SupplierContractListHandler : ListQueryHandler, IRequestHandler<SupplierContractListQuery, List<SupplierContractDto>>
{
    public SupplierContractListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractDto>> Handle(SupplierContractListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContracts.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractId)
                              .ProjectTo<SupplierContractDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}