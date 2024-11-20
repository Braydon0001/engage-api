using Engage.Application.Services.EngageMasterProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public class EngageMasterProductOptionsCodeQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class EngageMasterProductOptionsCodeQueryHandler : BaseQueryHandler, IRequestHandler<EngageMasterProductOptionsCodeQuery, List<OptionDto>>
{
    public EngageMasterProductOptionsCodeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(EngageMasterProductOptionsCodeQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageMasterProducts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(o => EF.Functions.Like(o.Code, $"%{request.Search}%") ||
                                             EF.Functions.Like(o.Name, $"%{request.Search}%"));
                                
        };


        return await queryable.OrderBy(e => e.Name)
                              .Select(e => new OptionDto
                                {
                                    Id = e.EngageMasterProductId,
                                    Name = e.Name + " - " + e.Code
                                })
                              .Take(100)
                              .ToListAsync(cancellationToken);

    }
}
