// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Queries;

public class SupplierContractDetailListQuery : IRequest<List<SupplierContractDetailDto>>
{
    public int SupplierContractId { get; set; }
}

public class SupplierContractDetailListHandler : ListQueryHandler, IRequestHandler<SupplierContractDetailListQuery, List<SupplierContractDetailDto>>
{
    public SupplierContractDetailListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractDetailDto>> Handle(SupplierContractDetailListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractDetails.AsQueryable().AsNoTracking();

        if (query.SupplierContractId > 0)
        {
            queryable = queryable.Where(e => e.SupplierContractId == query.SupplierContractId);
        }
        else
        {
            throw new Exception("Supplier Contract not found");
        }

        return await queryable.OrderBy(e => e.SupplierContractDetailId)
                              .ProjectTo<SupplierContractDetailDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}