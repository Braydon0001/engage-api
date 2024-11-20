namespace Engage.Application.Services.Stores.Queries;

public class StoreOptionVmQuery : IRequest<OptionDto>
{
    public int Id { get; set; }
}

public class StoreOptionVmQueryHandler : IRequestHandler<StoreOptionVmQuery, OptionDto>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public StoreOptionVmQueryHandler(IAppDbContext context, IMediator mediator, IUserService userService)
    {
        _context = context;
        _mediator = mediator;
        _userService = userService;
    }

    public async Task<OptionDto> Handle(StoreOptionVmQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Stores.AsNoTracking().AsQueryable();

        queryable = queryable.Where(e => e.StoreId == request.Id);

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.StoreId, Name = e.Name + " - " + e.EngageRegion.Name })
                              .FirstOrDefaultAsync(cancellationToken);
    }
}

