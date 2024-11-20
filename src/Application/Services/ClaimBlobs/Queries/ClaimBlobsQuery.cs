using Engage.Application.Services.EntityBlobs.Models;

namespace Engage.Application.Services.ClaimBlobs.Queries;

public class ClaimBlobsQuery : GetQuery, IRequest<ListResult<EntityBlobDto>>
{
    public int ClaimId { get; set; }
}

public class ClaimBlobsQueryHandler : IRequestHandler<ClaimBlobsQuery, ListResult<EntityBlobDto>>
{
    private readonly IAppDbContext _context;

    public ClaimBlobsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ListResult<EntityBlobDto>> Handle(ClaimBlobsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimBlobs.Where(e => e.ClaimId == request.ClaimId)
                                                .OrderByDescending(e => e.EntityBlobId)
                                                .Select(e => new EntityBlobDto(e))
                                                .ToListAsync(cancellationToken);
        return new ListResult<EntityBlobDto>(entities);
    }
}
