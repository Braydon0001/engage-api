namespace Engage.Application.Services.Options.Queries;

public class OptionQuery : GetByIdQuery, IRequest<OptionDto>
{
    public string Option { get; set; }
}

public class OptionQueryHandler : IRequestHandler<OptionQuery, OptionDto>
{
    private readonly IAppDbContext _context;

    public OptionQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OptionDto> Handle(OptionQuery request, CancellationToken cancellationToken)
    {
        var option = await OptionUtils.FindAsync(request.Id, request.Option, _context, cancellationToken);
        return new OptionDto(option.Id, option.Name);
    }
}

