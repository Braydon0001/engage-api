// auto-generated
namespace Engage.Application.Services.SupplierContractAmountTypes.Queries;

public class SupplierContractAmountTypeOptionListQuery : IRequest<List<SupplierContractAmountTypeOption>>
{ 

}

public class SupplierContractAmountTypeOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractAmountTypeOptionListQuery, List<SupplierContractAmountTypeOption>>
{
    public SupplierContractAmountTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractAmountTypeOption>> Handle(SupplierContractAmountTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractAmountTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractAmountTypeId)
                              .ProjectTo<SupplierContractAmountTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}