// auto-generated
namespace Engage.Application.Services.SupplierContractGroups.Queries;

public class SupplierContractGroupListQuery : IRequest<List<SupplierContractGroupDto>>
{

}

public class SupplierContractGroupListHandler : ListQueryHandler, IRequestHandler<SupplierContractGroupListQuery, List<SupplierContractGroupDto>>
{
    public SupplierContractGroupListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractGroupDto>> Handle(SupplierContractGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractGroupId)
                              .ProjectTo<SupplierContractGroupDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}