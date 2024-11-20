namespace Engage.Application.Services.SparSubProducts.Queries;

public class SparSubProductByProductQuery : IRequest<List<SparSubProductDto>>
{
    public int SparProductId { get; set; }
}
public class SparSubProductByProductHandler : BaseQueryHandler, IRequestHandler<SparSubProductByProductQuery, List<SparSubProductDto>>
{
    public SparSubProductByProductHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SparSubProductDto>> Handle(SparSubProductByProductQuery query, CancellationToken cancellationToken)
    {
        if (query.SparProductId < 1)
        {
            throw new Exception("Spar product Id not found");
        }

        var queryable = _context.SparSubProducts.AsNoTracking()
                                                .AsQueryable()
                                                .Where(e => e.SparProductId == query.SparProductId);

        return await queryable.OrderBy(e => e.SparSubProductId)
                              .ProjectTo<SparSubProductDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}