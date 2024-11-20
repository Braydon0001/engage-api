namespace Engage.Application.Services.Shared.Queries;

public abstract class BaseOptionListHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;

    protected BaseOptionListHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
