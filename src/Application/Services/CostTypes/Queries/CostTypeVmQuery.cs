namespace Engage.Application.Services.CostTypes.Queries;

public record CostTypeVmQuery(int Id) : IRequest<CostTypeVm>;

public record CostTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostTypeVmQuery, CostTypeVm>
{
    public async Task<CostTypeVm> Handle(CostTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CostTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CostTypeVm>(entity);
    }
}