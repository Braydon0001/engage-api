namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int MasterProductId { get; set; }
}

public class EngageVariantProductOptionsQueryHandler : BaseQueryHandler, IRequestHandler<EngageVariantProductOptionsQuery, List<OptionDto>>
{
    public EngageVariantProductOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<List<OptionDto>> Handle(EngageVariantProductOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageVariantProducts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(o => EF.Functions.Like(o.Code, $"%{request.Search}%") ||
                                             EF.Functions.Like(o.Name, $"%{request.Search}%") ||
                                             EF.Functions.Like(o.UnitBarcode, $"%{request.Search}%"));
        }

        return await queryable.OrderBy(e => e.Name)
                              .Select(e => new OptionDto(e.EngageVariantProductId, e.Code + " / " + e.Name))
                              .Take(100)
                              .ToListAsync(cancellationToken);
    }
}
