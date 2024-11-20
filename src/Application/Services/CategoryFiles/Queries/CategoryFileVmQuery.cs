namespace Engage.Application.Services.CategoryFiles.Queries;

public record CategoryFileVmQuery(int Id) : IRequest<CategoryFileVm>;

public record CategoryFileVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileVmQuery, CategoryFileVm>
{
    public async Task<CategoryFileVm> Handle(CategoryFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryFiles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CategoryFileType)
                             .Include(e => e.Store)
                             .Include(e => e.CategoryGroup)
                             .Include(e => e.CategorySubGroup);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryFileId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryFileVm>(entity);
    }
}