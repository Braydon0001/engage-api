using Engage.Application.Services.SubContractorBrands.Queries;

namespace Engage.Application.Services.SupplierBudgets.Queries;

public class SubContractorBrandListQuery : IRequest<List<SubContractorBrandDto>>
{
    public int Id { get; set; }
}

public class SubContractorBrandListHandler : ListQueryHandler, IRequestHandler<SubContractorBrandListQuery, List<SubContractorBrandDto>>
{
    public SubContractorBrandListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SubContractorBrandDto>> Handle(SubContractorBrandListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SubContractorBrands.AsQueryable().AsNoTracking();

        if (query.Id > 0)
        {
            queryable = queryable.Where(e => e.ParentId == query.Id);
        }

        return await queryable.OrderBy(e => e.SubContractorBrandId)
                              .ProjectTo<SubContractorBrandDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}