// auto-generated
namespace Engage.Application.Services.SupplierContractAmountTypes.Queries;

public class SupplierContractAmountTypeListQuery : IRequest<List<SupplierContractAmountTypeDto>>
{

}

public class SupplierContractAmountTypeListHandler : ListQueryHandler, IRequestHandler<SupplierContractAmountTypeListQuery, List<SupplierContractAmountTypeDto>>
{
    public SupplierContractAmountTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractAmountTypeDto>> Handle(SupplierContractAmountTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractAmountTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractAmountTypeId)
                              .ProjectTo<SupplierContractAmountTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}