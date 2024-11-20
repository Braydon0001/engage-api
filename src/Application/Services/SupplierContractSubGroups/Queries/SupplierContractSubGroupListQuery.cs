// auto-generated
namespace Engage.Application.Services.SupplierContractSubGroups.Queries;

public class SupplierContractSubGroupListQuery : IRequest<List<SupplierContractSubGroupDto>>
{

}

public class SupplierContractSubGroupListHandler : ListQueryHandler, IRequestHandler<SupplierContractSubGroupListQuery, List<SupplierContractSubGroupDto>>
{
    public SupplierContractSubGroupListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractSubGroupDto>> Handle(SupplierContractSubGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractSubGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractSubGroupId)
                              .ProjectTo<SupplierContractSubGroupDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}