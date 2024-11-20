namespace Engage.Application.CQRS.Queries;

public class ListQueryHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;

    public ListQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
