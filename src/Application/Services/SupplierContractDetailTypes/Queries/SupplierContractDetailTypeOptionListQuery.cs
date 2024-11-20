// auto-generated
namespace Engage.Application.Services.SupplierContractDetailTypes.Queries;

public class SupplierContractDetailTypeOptionListQuery : IRequest<List<SupplierContractDetailTypeOption>>
{ 

}

public class SupplierContractDetailTypeOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractDetailTypeOptionListQuery, List<SupplierContractDetailTypeOption>>
{
    public SupplierContractDetailTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractDetailTypeOption>> Handle(SupplierContractDetailTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractDetailTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractDetailTypeId)
                              .ProjectTo<SupplierContractDetailTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}