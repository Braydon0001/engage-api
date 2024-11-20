// auto-generated
namespace Engage.Application.Services.ProductYears.Queries;

public class ProductYearVmQuery : IRequest<ProductYearVm>
{
    public int Id { get; set; }
}

public class ProductYearVmHandler : VmQueryHandler, IRequestHandler<ProductYearVmQuery, ProductYearVm>
{
    public ProductYearVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductYearVm> Handle(ProductYearVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductYears.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductYearId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductYearVm>(entity);
    }
}