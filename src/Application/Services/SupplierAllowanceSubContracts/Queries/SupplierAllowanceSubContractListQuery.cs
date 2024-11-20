namespace Engage.Application.Services.SupplierAllowanceSubContracts.Queries;

public class SupplierAllowanceSubContractListQuery : IRequest<List<SupplierAllowanceSubContractDto>>
{
    public int SupplierAllowanceContractId { get; set; }
}

public class SupplierAllowanceSubContractListHandler : ListQueryHandler, IRequestHandler<SupplierAllowanceSubContractListQuery, List<SupplierAllowanceSubContractDto>>
{
    public SupplierAllowanceSubContractListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierAllowanceSubContractDto>> Handle(SupplierAllowanceSubContractListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierAllowanceSubContracts.Where(s => s.SupplierAllowanceContractId == query.SupplierAllowanceContractId)
                                                              .AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Vendor)
                              .ThenBy(e => e.SupplierAllowanceContractId)
                              .ProjectTo<SupplierAllowanceSubContractDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}