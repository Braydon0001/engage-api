namespace Engage.Application.Services.ExternalUserTypes.Queries;

public record ExternalUserTypeVmQuery(int Id) : IRequest<ExternalUserTypeVm>;

public record ExternalUserTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ExternalUserTypeVmQuery, ExternalUserTypeVm>
{
    public async Task<ExternalUserTypeVm> Handle(ExternalUserTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ExternalUserTypes.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.ExternalUserTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ExternalUserTypeVm>(entity);
    }
}