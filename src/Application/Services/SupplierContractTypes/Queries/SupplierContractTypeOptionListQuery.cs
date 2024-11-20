// auto-generated
namespace Engage.Application.Services.SupplierContractTypes.Queries;

public class SupplierContractTypeOptionListQuery : IRequest<List<SupplierContractTypeOption>>
{ 

}

public class SupplierContractTypeOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractTypeOptionListQuery, List<SupplierContractTypeOption>>
{
    public SupplierContractTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractTypeOption>> Handle(SupplierContractTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractTypeId)
                              .ProjectTo<SupplierContractTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}