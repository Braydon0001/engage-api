namespace Engage.Application.Services.SupplierBudgets.Queries;

public class SupplierBudgetListBySupplierContractQuery : IRequest<List<SupplierBudgetDto>>
{
    public int SupplierContractId { get; set; }
    public int SupplierBudgetVersionId { get; set; }
    public int SupplierBudgetTypeId { get; set; }
}
public class SupplierBudgetListBySupplierContractHandler : ListQueryHandler, IRequestHandler<SupplierBudgetListBySupplierContractQuery, List<SupplierBudgetDto>>
{
    public SupplierBudgetListBySupplierContractHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetDto>> Handle(SupplierBudgetListBySupplierContractQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgets.AsQueryable().AsNoTracking();

        if (query.SupplierContractId > 0)
        {
            queryable = queryable.Where(e => e.SupplierContractDetail.SupplierContractId == query.SupplierContractId);
        }
        else
        {
            throw new Exception("Supplier Contract not found");
        }

        return await queryable.OrderBy(e => e.SupplierBudgetId)
                              .ProjectTo<SupplierBudgetDto>(_mapper.ConfigurationProvider)
                              .ToListAsync();
    }
}