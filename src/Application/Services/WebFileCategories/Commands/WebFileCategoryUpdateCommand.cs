// auto-generated
namespace Engage.Application.Services.WebFileCategories.Commands;

public class WebFileCategoryUpdateCommand : WebFileCategoryCommand, IMapTo<WebFileCategory>, IRequest<WebFileCategory>
{
    public int Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFileCategoryUpdateCommand, WebFileCategory>();
    }
}

public class WebFileCategoryUpdateHandler : UpdateHandler, IRequestHandler<WebFileCategoryUpdateCommand, WebFileCategory>
{
    public WebFileCategoryUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebFileCategory> Handle(WebFileCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFileCategories.SingleOrDefaultAsync(e => e.WebFileCategoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateWebFileCategoryValidator : WebFileCategoryValidator<WebFileCategoryUpdateCommand>
{
    public UpdateWebFileCategoryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();

    }
}