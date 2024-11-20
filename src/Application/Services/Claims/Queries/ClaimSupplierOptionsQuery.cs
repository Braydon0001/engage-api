using Engage.Application.Services.Claims.Models;

namespace Engage.Application.Services.Claims.Queries;

public class ClaimSupplierOptionsQuery : GetQuery, IRequest<List<ClaimSupplierOptionDto>>
{
    public bool IsDairy { get; set; }
    public bool IsEmployeeClaim { get; set; }
}

public class ClaimSupplierOptionsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimSupplierOptionsQuery, List<ClaimSupplierOptionDto>>
{
    public ClaimSupplierOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ClaimSupplierOptionDto>> Handle(ClaimSupplierOptionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Suppliers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        if (request.IsDairy)
        {
            query = query.Where(e => e.IsDairy == true);
        }

        if (request.IsEmployeeClaim)
        {
            query = query.Where(e => e.IsEmployeeClaim == true);
        }

        return await query.Where(e => e.ClaimModuleEnabled && e.Disabled == false)
                          .ProjectTo<ClaimSupplierOptionDto>(_mapper.ConfigurationProvider)
                          .Take(100)
                          .OrderBy(e => e.Name)
                          .ToListAsync(cancellationToken);
    }
}
