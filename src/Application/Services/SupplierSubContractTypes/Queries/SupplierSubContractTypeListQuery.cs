// auto-generated
namespace Engage.Application.Services.SupplierSubContractTypes.Queries;

public class SupplierSubContractTypeListQuery : IRequest<List<SupplierSubContractTypeDto>>
{

}

public class SupplierSubContractTypeListHandler : ListQueryHandler, IRequestHandler<SupplierSubContractTypeListQuery, List<SupplierSubContractTypeDto>>
{
    public SupplierSubContractTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractTypeDto>> Handle(SupplierSubContractTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierSubContractTypeId)
                              .ProjectTo<SupplierSubContractTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}