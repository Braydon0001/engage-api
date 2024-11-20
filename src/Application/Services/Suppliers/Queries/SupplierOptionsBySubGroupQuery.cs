namespace Engage.Application.Services.Suppliers.Queries;

public class SupplierOptionsBySubGroupQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int EngageSubGroupId { get; set; }
}

public class SupplierOptionsBySubGroupQueryHandler : BaseQueryHandler, IRequestHandler<SupplierOptionsBySubGroupQuery, List<OptionDto>>
{
    public SupplierOptionsBySubGroupQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<List<OptionDto>> Handle(SupplierOptionsBySubGroupQuery request, CancellationToken cancellationToken)
    {
        return await _context.Suppliers.Join(_context.EngageMasterProducts.Where(e => e.EngageSubCategory.EngageCategory.EngageSubGroupId == request.EngageSubGroupId),
                                                          supplier => supplier.SupplierId,
                                                          product => product.SupplierId,
                                                          (supplier, product) => supplier)
                                        .Distinct()
                                        .OrderBy(e => e.Name)
                                        .Select(e => new OptionDto(e.SupplierId, e.Name))
                                        .ToListAsync(cancellationToken);
    }
}
