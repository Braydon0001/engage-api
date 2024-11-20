// auto-generated
namespace Engage.Application.Services.SupplierSubContractTypes.Queries;

public class SupplierSubContractTypeOptionListQuery : IRequest<List<SupplierSubContractTypeOption>>
{ 

}

public class SupplierSubContractTypeOptionListHandler : ListQueryHandler, IRequestHandler<SupplierSubContractTypeOptionListQuery, List<SupplierSubContractTypeOption>>
{
    public SupplierSubContractTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractTypeOption>> Handle(SupplierSubContractTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierSubContractTypeId)
                              .ProjectTo<SupplierSubContractTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}