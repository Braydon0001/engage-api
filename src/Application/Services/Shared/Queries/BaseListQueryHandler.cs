namespace Engage.Application.Services.Shared.Queries;

public abstract class BaseListQueryHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;

    protected BaseListQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
