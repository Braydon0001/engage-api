namespace Engage.Application.Services.Shared.Commands;

public abstract class BaseCreateCommandHandler
{
    protected readonly IAppDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;

    protected BaseCreateCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    protected BaseCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }
}
