// auto-generated

namespace Engage.Application.Services.SupplierContracts.Queries;

public class SupplierContractOptionListQuery : IRequest<List<SupplierContractOption>>
{
    public int? SupplierId { get; set; }
}

public class SupplierContractOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractOptionListQuery, List<SupplierContractOption>>
{
    public SupplierContractOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractOption>> Handle(SupplierContractOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContracts.AsQueryable().AsNoTracking();

        if (query.SupplierId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierId == query.SupplierId);
        }

        return await queryable.OrderBy(e => e.SupplierContractId)
                        .ProjectTo<SupplierContractOption>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
    }
}