namespace Engage.Application.Services.CreditorStatuses.Queries;

public record CreditorStatusVmQuery(int Id) : IRequest<CreditorStatusVm>;

public record CreditorStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusVmQuery, CreditorStatusVm>
{
    public async Task<CreditorStatusVm> Handle(CreditorStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CreditorStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CreditorStatusVm>(entity);
    }
}