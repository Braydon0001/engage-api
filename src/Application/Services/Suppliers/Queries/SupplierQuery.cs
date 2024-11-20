using Engage.Application.Services.Suppliers.Models;

namespace Engage.Application.Services.Suppliers.Queries
{
    public class SupplierQuery : GetByIdQuery, IRequest<SupplierDto>
    {
    }

    public class SupplierQueryHandler : BaseQueryHandler, IRequestHandler<SupplierQuery, SupplierDto>
    {
        public SupplierQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<SupplierDto> Handle(SupplierQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Suppliers.Include(x => x.SupplierSupplierTypes)
                                                 .ThenInclude(x => x.SupplierType)
                                                 .Include(x => x.SupplierEngageBrands)
                                                 .ThenInclude(x => x.EngageBrand)
                                                 .FirstOrDefaultAsync(x => x.SupplierId == request.Id, cancellationToken);

            return _mapper.Map<Supplier, SupplierDto>(entity);
        }
    }
}
