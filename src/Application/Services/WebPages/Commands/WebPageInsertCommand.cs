// auto-generated
namespace Engage.Application.Services.WebPages.Commands;

public class WebPageInsertCommand : IMapTo<WebPage>, IRequest<WebPage>
{
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPageInsertCommand, WebPage>();
    }
}

public class WebPageInsertHandler : InsertHandler, IRequestHandler<WebPageInsertCommand, WebPage>
{
    public WebPageInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebPage> Handle(WebPageInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<WebPageInsertCommand, WebPage>(command);
        
        _context.WebPages.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class WebPageInsertValidator : AbstractValidator<WebPageInsertCommand>
{
    public WebPageInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}