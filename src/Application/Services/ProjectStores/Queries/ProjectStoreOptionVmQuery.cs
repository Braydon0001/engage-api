namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStoreOptionVmQuery : GetQuery, IRequest<OptionDto>
{
    public int Id { get; set; }
}

public class ProjectStoreOptionVmQueryHandler : IRequestHandler<ProjectStoreOptionVmQuery, OptionDto>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public ProjectStoreOptionVmQueryHandler(IAppDbContext context, IMediator mediator, IUserService userService)
    {
        _context = context;
        _mediator = mediator;
        _userService = userService;
    }

    public async Task<OptionDto> Handle(ProjectStoreOptionVmQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectStores.AsQueryable();

        if (request.Id > 0)
        {
            queryable = queryable.Where(e => e.ProjectId == request.Id);
        }

        return await queryable.Select(e => new OptionDto { Id = e.ProjectId, Name = e.Name + " - " + e.Store.Name })
                              .FirstOrDefaultAsync(cancellationToken);
    }
}

