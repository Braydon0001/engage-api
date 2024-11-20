using Engage.Application.Services.Vendors.Models;

namespace Engage.Application.Services.Vendors.Queries;

public class VendorOptionQuery : IRequest<List<VendorOptionDto>>
{
}
public record VendorOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<VendorOptionQuery, List<VendorOptionDto>>
{
    public async Task<List<VendorOptionDto>> Handle(VendorOptionQuery request, CancellationToken cancellationToken)
    {
        var queryable = Context.Vendors.AsNoTracking().AsQueryable();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<VendorOptionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}