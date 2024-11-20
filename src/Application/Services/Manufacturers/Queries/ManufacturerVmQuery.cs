using Engage.Application.Services.Manufacturers.Models;

namespace Engage.Application.Services.Manufacturers.Queries;

public class ManufacturerVmQuery: GetByIdQuery, IRequest<ManufacturerVm>
{
}

public class ManufacturerVmQueryHandler : BaseQueryHandler, IRequestHandler<ManufacturerVmQuery, ManufacturerVm>
{
    public ManufacturerVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ManufacturerVm> Handle(ManufacturerVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Manufacturers.Include(e => e.Supplier)
                                                 .Include(e => e.EngageRegion)                     
                                                 .SingleAsync(e => e.ManufacturerId == request.Id, cancellationToken);

        return _mapper.Map<ManufacturerVm>(entity);
    }
}
