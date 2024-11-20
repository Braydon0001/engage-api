namespace Engage.Application.Services.SupplierSubContractDetails.Queries;

public class SupplierSubContractDetailBySubContractTypeQuery : IRequest<List<SupplierSubContractDetailDto>>
{
    public int SupplierSubContractTypeId { get; set; }
}
public class SupplierSubContractDetailBySubContractTypeHandler : ListQueryHandler, IRequestHandler<SupplierSubContractDetailBySubContractTypeQuery, List<SupplierSubContractDetailDto>>
{
    public SupplierSubContractDetailBySubContractTypeHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierSubContractDetailDto>> Handle(SupplierSubContractDetailBySubContractTypeQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractDetails.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.SupplierSubContractTypeId == request.SupplierSubContractTypeId);

        return await queryable.OrderBy(e => e.SupplierSubContractDetailId)
                              .ProjectTo<SupplierSubContractDetailDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}