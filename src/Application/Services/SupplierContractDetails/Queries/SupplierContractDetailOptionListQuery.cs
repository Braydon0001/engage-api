// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Queries;

public class SupplierContractDetailOptionListQuery : IRequest<List<SupplierContractDetailOption>>
{
    public int? SupplierContractId { get; set; }
}

public class SupplierContractDetailOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractDetailOptionListQuery, List<SupplierContractDetailOption>>
{
    public SupplierContractDetailOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractDetailOption>> Handle(SupplierContractDetailOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractDetails.AsQueryable().AsNoTracking();

        if (query.SupplierContractId != null)
        {
            queryable = queryable.Where(e => e.SupplierContractId == query.SupplierContractId);
        }

        return await queryable.OrderBy(e => e.SupplierContractDetailId)
                              .ProjectTo<SupplierContractDetailOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}