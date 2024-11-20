// auto-generated
namespace Engage.Application.Services.SupplierContractSubGroups.Queries;

public class SupplierContractSubGroupOptionListQuery : IRequest<List<SupplierContractSubGroupOption>>
{
    public int? SupplierContractGroupId { get; set; }
}

public class SupplierContractSubGroupOptionListHandler : ListQueryHandler, IRequestHandler<SupplierContractSubGroupOptionListQuery, List<SupplierContractSubGroupOption>>
{
    public SupplierContractSubGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierContractSubGroupOption>> Handle(SupplierContractSubGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractSubGroups.AsQueryable().AsNoTracking();

        if (query.SupplierContractGroupId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierContractGroupId == query.SupplierContractGroupId);
        }

        return await queryable.OrderBy(e => e.SupplierContractSubGroupId)
                              .ProjectTo<SupplierContractSubGroupOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}