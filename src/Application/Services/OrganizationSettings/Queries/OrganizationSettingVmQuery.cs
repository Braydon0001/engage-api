using Engage.Application.Services.Organizations.Queries;
namespace Engage.Application.Services.OrganizationSettings.Queries;

public record OrganizationSettingVmQuery(int OrganizationId) : IRequest<OrganizationWithSettingVm>;

public record OrganizationSettingVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganizationSettingVmQuery, OrganizationWithSettingVm>
{
    public async Task<OrganizationWithSettingVm> Handle(OrganizationSettingVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Organizations.AsQueryable()
                                             .Include(o => o.OrganizationSetting)
                                             .AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.OrganizationId == query.OrganizationId, cancellationToken);

        return entity == null ? null : Mapper.Map<OrganizationWithSettingVm>(entity);
    }
}