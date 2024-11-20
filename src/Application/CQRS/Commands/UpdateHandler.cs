namespace Engage.Application.CQRS.Commands;

public abstract class UpdateHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;

    protected UpdateHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
