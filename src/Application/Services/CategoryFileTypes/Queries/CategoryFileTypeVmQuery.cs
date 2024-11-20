namespace Engage.Application.Services.CategoryFileTypes.Queries;

public record CategoryFileTypeVmQuery(int Id) : IRequest<CategoryFileTypeVm>;

public record CategoryFileTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileTypeVmQuery, CategoryFileTypeVm>
{
    public async Task<CategoryFileTypeVm> Handle(CategoryFileTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryFileTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryFileTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryFileTypeVm>(entity);
    }
}