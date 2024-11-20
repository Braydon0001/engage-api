namespace Engage.Application.Services.CategoryFileTargets.Queries;

public class CategoryFileTargetModelQuery : IRequest<JsonRule>
{
    public int Id { get; set; }
}

public class CategoryFileTargetModelHandler : IRequestHandler<CategoryFileTargetModelQuery, JsonRule>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryFileTargetModelHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<JsonRule> Handle(CategoryFileTargetModelQuery query, CancellationToken cancellationToken)
    {
        var entity = await _context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        return entity.TargetRule ?? new JsonRule();
    }
}
