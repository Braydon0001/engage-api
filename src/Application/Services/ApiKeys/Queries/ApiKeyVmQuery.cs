namespace Engage.Application.Services.ApiKeys.Queries;

public record ApiKeyVmQuery(int Id) : IRequest<ApiKeyVm>;

public record ApiKeyVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ApiKeyVmQuery, ApiKeyVm>
{
    public async Task<ApiKeyVm> Handle(ApiKeyVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ApiKeys.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.ApiKeyId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ApiKeyVm>(entity);
    }
}