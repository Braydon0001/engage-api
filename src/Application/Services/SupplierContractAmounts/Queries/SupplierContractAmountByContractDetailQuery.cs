namespace Engage.Application.Services.SupplierContractAmounts.Queries;

public class SupplierContractAmountByContractDetailQuery : IRequest<List<SupplierContractAmountDto>>
{
    public int SupplierSubContractDetailId { get; set; }
}
public class SupplierContractAmountByContractDetailHandler : ListQueryHandler, IRequestHandler<SupplierContractAmountByContractDetailQuery, List<SupplierContractAmountDto>>
{
    public SupplierContractAmountByContractDetailHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractAmountDto>> Handle(SupplierContractAmountByContractDetailQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractAmounts.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.SupplierSubContractDetailId == request.SupplierSubContractDetailId);

        return await queryable.OrderBy(e => e.SupplierContractAmountId)
                              .ProjectTo<SupplierContractAmountDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
