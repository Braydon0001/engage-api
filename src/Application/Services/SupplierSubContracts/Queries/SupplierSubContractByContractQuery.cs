namespace Engage.Application.Services.SupplierSubContracts.Queries;

public class SupplierSubContractByContractQuery : IRequest<List<SupplierSubContractDto>>
{
    public int SupplierContractId { get; set; }
}
public class SupplierSubContractByContractHandler : ListQueryHandler, IRequestHandler<SupplierSubContractByContractQuery, List<SupplierSubContractDto>>
{
    public SupplierSubContractByContractHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractDto>> Handle(SupplierSubContractByContractQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContracts.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.SupplierContractId == request.SupplierContractId);

        return await queryable.OrderBy(e => e.SupplierSubContractId)
                              .ProjectTo<SupplierSubContractDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
