namespace Engage.Application.CQRS.Queries;

public class VmQueryHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;

    public VmQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
