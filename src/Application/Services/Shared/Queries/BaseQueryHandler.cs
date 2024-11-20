namespace Engage.Application.Services.Shared.Queries;

public abstract class BaseQueryHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;

    protected BaseQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
