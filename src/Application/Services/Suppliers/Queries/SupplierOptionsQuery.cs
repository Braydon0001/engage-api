namespace Engage.Application.Services.Suppliers.Queries;

public class SupplierOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? ExcludeId { get; set; }
}

public class SupplierOptionsQueryHandler : IRequestHandler<SupplierOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public SupplierOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(SupplierOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Suppliers.AsQueryable();

        if (request.ExcludeId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierId != request.ExcludeId);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.SupplierId, Name = e.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}

