namespace Engage.Application.Services.EngageRegions.Queries;

public class EngageRegionOptionVmQuery : IRequest<OptionDto>
{
    public int Id { get; set; }
}

public class EngageRegionOptionVmQueryHandler : IRequestHandler<EngageRegionOptionVmQuery, OptionDto>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;

    public EngageRegionOptionVmQueryHandler(IAppDbContext context, IMediator mediator, IUserService user)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
    }

    public async Task<OptionDto> Handle(EngageRegionOptionVmQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageRegions.Where(e => e.Disabled == false && e.Deleted == false);

        queryable = queryable.Where(e => e.Id == request.Id);

        queryable = queryable.OrderBy(e => e.Name);

        return await queryable.Select(e => new OptionDto(e.Id, e.Name))
                              .FirstOrDefaultAsync(cancellationToken);
    }
}
