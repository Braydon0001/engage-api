namespace Engage.Application.Services.ClaimStatuses.Queries;

public class ClaimStatusOptionsQuery : IRequest<List<OptionDto>>
{
    public bool IsProcessable { get; set; }
}

public class ClaimStatusOptionsQueryHandler : IRequestHandler<ClaimStatusOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ClaimStatusOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;        
    }

    public async Task<List<OptionDto>> Handle(ClaimStatusOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ClaimStatuses.Where(e => e.Deleted == false);

        if (request.IsProcessable)
        {
                queryable = queryable.Where(e => e.Id == (int)ClaimStatusId.Unapproved || 
                                                 e.Id == (int)ClaimStatusId.Approved);
            
        }

        return await queryable.Select(e => new OptionDto(e.Id, e.Name))
                              .ToListAsync(cancellationToken);
    }
}
