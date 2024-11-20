namespace Engage.Application.Services.SupplierSubContractDetailTypes.Queries;

public class SupplierSubContractDetailTypeOptionListQuery : IRequest<List<SupplierSubContractDetailTypeOption>>
{

}
public class SupplierSubContractDetailTypeOptionListHandler : ListQueryHandler, IRequestHandler<SupplierSubContractDetailTypeOptionListQuery, List<SupplierSubContractDetailTypeOption>>
{
    public SupplierSubContractDetailTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractDetailTypeOption>> Handle(SupplierSubContractDetailTypeOptionListQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractDetailTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.SupplierSubContractDetailTypeId)
                              .ProjectTo<SupplierSubContractDetailTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}