// auto-generated
namespace Engage.Application.Services.WebFileCategories.Commands;

public class WebFileCategoryInsertCommand : WebFileCategoryCommand, IMapTo<WebFileCategory>, IRequest<WebFileCategory>
{

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFileCategoryInsertCommand, WebFileCategory>();
    }
}

public class WebFileCategoryInsertHandler : InsertHandler, IRequestHandler<WebFileCategoryInsertCommand, WebFileCategory>
{
    public WebFileCategoryInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<WebFileCategory> Handle(WebFileCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<WebFileCategoryInsertCommand, WebFileCategory>(command);
        _context.WebFileCategories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class WebFileCategoryInsertValidator : WebFileCategoryValidator<WebFileCategoryInsertCommand>
{
    public WebFileCategoryInsertValidator()
    {

    }
}