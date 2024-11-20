using Engage.Application.Services.ClaimTypes.Models;

namespace Engage.Application.Services.ClaimTypes.Queries;

public class ClaimTypeOptionsQuery : GetQuery, IRequest<List<ClaimTypeVm>>
{
    public bool? IsDairy { get; set; }
    public int? ClaimClassificationId { get; set; }
}

public class ClaimTypeOptionsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimTypeOptionsQuery, List<ClaimTypeVm>>
{
    public ClaimTypeOptionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ClaimTypeVm>> Handle(ClaimTypeOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ClaimTypes.AsQueryable();

        if (request.IsDairy.HasValue)
        {
            queryable = queryable.Where(e => e.IsDairy == request.IsDairy);
        }

        if (request.ClaimClassificationId.HasValue)
        {
            var claimTypeIds = await _context.ClaimClassificationTypes
                                            .Where(e => e.ClaimClassificationId == request.ClaimClassificationId)
                                            .Select(e => e.ClaimTypeId)
                                            .ToListAsync(cancellationToken);

            if (claimTypeIds.Any())
            {
                queryable = queryable.Where(e => claimTypeIds.Contains(e.ClaimTypeId));
            }
        }

        return await queryable.Where(e => e.Disabled == false)
                              .OrderBy(e => e.Name)
                              .ProjectTo<ClaimTypeVm>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
