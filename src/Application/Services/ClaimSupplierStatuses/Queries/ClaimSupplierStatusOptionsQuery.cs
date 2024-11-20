namespace Engage.Application.Services.ClaimSupplierStatuses.Queries;

public class ClaimSupplierStatusOptionsQuery : IRequest<List<OptionDto>>
{
    public bool IsProcessable { get; set; }
}

public class ClaimSupplierStatusOptionsQueryHandler : IRequestHandler<ClaimSupplierStatusOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    
    public ClaimSupplierStatusOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ClaimSupplierStatusOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ClaimSupplierStatuses.Where(e => e.Deleted == false);

        if (request.IsProcessable)
        {
            
                queryable = queryable.Where(e => e.Id == (int)ClaimSupplierStatusId.Unapproved || 
                                                 e.Id == (int)ClaimSupplierStatusId.Disputed);            
        }

        return await queryable.Select(e => new OptionDto(e.Id, e.Name))
                              .ToListAsync(cancellationToken);
    }
}
