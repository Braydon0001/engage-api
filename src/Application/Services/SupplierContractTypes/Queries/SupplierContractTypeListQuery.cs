// auto-generated
namespace Engage.Application.Services.SupplierContractTypes.Queries;

public class SupplierContractTypeListQuery : IRequest<List<SupplierContractTypeDto>>
{

}

public class SupplierContractTypeListHandler : ListQueryHandler, IRequestHandler<SupplierContractTypeListQuery, List<SupplierContractTypeDto>>
{
    public SupplierContractTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractTypeDto>> Handle(SupplierContractTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractTypeId)
                              .ProjectTo<SupplierContractTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}