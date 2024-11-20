namespace Engage.Application.Services.Claims.Queries;

public class ClaimOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class ClaimOptionsQueryHandler : IRequestHandler<ClaimOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public ClaimOptionsQueryHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<List<OptionDto>> Handle(ClaimOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Claims.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.ClaimNumber, $"%{request.Search}%"))
                                            || (EF.Functions.Like(e.ClaimReference, $"%{request.Search}%"))
                                            );
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.ClaimId, Name = e.ClaimNumber + " - " + e.Store.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}
