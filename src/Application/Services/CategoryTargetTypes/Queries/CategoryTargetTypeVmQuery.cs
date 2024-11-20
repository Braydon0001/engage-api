namespace Engage.Application.Services.CategoryTargetTypes.Queries;

public record CategoryTargetTypeVmQuery(int Id) : IRequest<CategoryTargetTypeVm>;

public record CategoryTargetTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetTypeVmQuery, CategoryTargetTypeVm>
{
    public async Task<CategoryTargetTypeVm> Handle(CategoryTargetTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargetTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CategoryTargetTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CategoryTargetTypeVm>(entity);
    }
}