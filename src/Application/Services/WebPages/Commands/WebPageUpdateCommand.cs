// auto-generated
namespace Engage.Application.Services.WebPages.Commands;

public class WebPageUpdateCommand : IMapTo<WebPage>, IRequest<WebPage>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPageUpdateCommand, WebPage>();
    }
}

public class WebPageUpdateHandler : UpdateHandler, IRequestHandler<WebPageUpdateCommand, WebPage>
{
    public WebPageUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebPage> Handle(WebPageUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebPages.SingleOrDefaultAsync(e => e.WebPageId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateWebPageValidator : AbstractValidator<WebPageUpdateCommand>
{
    public UpdateWebPageValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}