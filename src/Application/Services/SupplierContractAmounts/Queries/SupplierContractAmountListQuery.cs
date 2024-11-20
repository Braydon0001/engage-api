// auto-generated
namespace Engage.Application.Services.SupplierContractAmounts.Queries;

public class SupplierContractAmountListQuery : IRequest<List<SupplierContractAmountDto>>
{

}

public class SupplierContractAmountListHandler : ListQueryHandler, IRequestHandler<SupplierContractAmountListQuery, List<SupplierContractAmountDto>>
{
    public SupplierContractAmountListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractAmountDto>> Handle(SupplierContractAmountListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractAmounts.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierContractAmountId)
                              .ProjectTo<SupplierContractAmountDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}