namespace Engage.Application.Services.SupplierSubContractDetailTypes.Queries;

public class SupplierSubContractDetailTypeListQuery : IRequest<List<SupplierSubContractDetailTypeDto>>
{
}
public class SupplierSubContractDetailTypeListHandler : ListQueryHandler, IRequestHandler<SupplierSubContractDetailTypeListQuery, List<SupplierSubContractDetailTypeDto>>
{
    public SupplierSubContractDetailTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractDetailTypeDto>> Handle(SupplierSubContractDetailTypeListQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractDetailTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.SupplierSubContractDetailTypeId)
                              .ProjectTo<SupplierSubContractDetailTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}