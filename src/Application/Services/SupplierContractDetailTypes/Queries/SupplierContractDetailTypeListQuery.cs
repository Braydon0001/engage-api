// auto-generated
namespace Engage.Application.Services.SupplierContractDetailTypes.Queries;

public class SupplierContractDetailTypeListQuery : IRequest<List<SupplierContractDetailTypeDto>>
{

}

public class SupplierContractDetailTypeListHandler : ListQueryHandler, IRequestHandler<SupplierContractDetailTypeListQuery, List<SupplierContractDetailTypeDto>>
{
    public SupplierContractDetailTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractDetailTypeDto>> Handle(SupplierContractDetailTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractDetailTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractDetailTypeId)
                              .ProjectTo<SupplierContractDetailTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}