namespace Engage.Application.CQRS.Commands;

public abstract class InsertHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;

    protected InsertHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
