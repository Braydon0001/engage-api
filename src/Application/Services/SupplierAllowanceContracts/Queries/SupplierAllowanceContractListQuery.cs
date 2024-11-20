namespace Engage.Application.Services.SupplierAllowanceContracts.Queries;

public class SupplierAllowanceContractListQuery : IRequest<List<SupplierAllowanceContractDto>>
{

}

public class SupplierAllowanceContractListHandler : ListQueryHandler, IRequestHandler<SupplierAllowanceContractListQuery, List<SupplierAllowanceContractDto>>
{
    public SupplierAllowanceContractListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierAllowanceContractDto>> Handle(SupplierAllowanceContractListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierAllowanceContracts.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Vendor)
                              .ThenBy(e => e.SupplierId)
                              .ProjectTo<SupplierAllowanceContractDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}