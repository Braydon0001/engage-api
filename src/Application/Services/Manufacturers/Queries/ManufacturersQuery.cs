using Engage.Application.Services.Manufacturers.Models;

namespace Engage.Application.Services.Manufacturers.Queries;

public class ManufacturersQuery : GetQuery, IRequest<ListResult<ManufacturerDto>>
{
    public int SupplierId { get; set; }
}

public class ManufacturersQueryHandler : BaseQueryHandler, IRequestHandler<ManufacturersQuery, ListResult<ManufacturerDto>>
{
    public ManufacturersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ManufacturerDto>> Handle(ManufacturersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Manufacturers.Where(e => e.SupplierId == request.SupplierId)
                                                   .OrderBy(e => e.Name)
                                                   .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<ManufacturerDto>(entities);
    }
}
