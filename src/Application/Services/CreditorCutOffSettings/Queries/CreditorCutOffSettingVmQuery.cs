namespace Engage.Application.Services.CreditorCutOffSettings.Queries;

public record CreditorCutOffSettingVmQuery(int Id) : IRequest<CreditorCutOffSettingVm>;

public record CreditorCutOffSettingVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorCutOffSettingVmQuery, CreditorCutOffSettingVm>
{
    public async Task<CreditorCutOffSettingVm> Handle(CreditorCutOffSettingVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorCutOffSettings.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.CreditorCutOffSettingId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CreditorCutOffSettingVm>(entity);
    }
}