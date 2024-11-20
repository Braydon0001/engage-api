using Engage.Application.Services.Settings.Models;
using Engage.Application.Services.Shared.Models;

namespace Engage.Application.Services.Settings.Queries;

public class TenantSettingsQuery : IRequest<ListResult<TenantSettingDto>>
{
}

public class TenantSettingsQueryHandler : BaseQueryHandler, IRequestHandler<TenantSettingsQuery, ListResult<TenantSettingDto>>
{
    public TenantSettingsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<TenantSettingDto>> Handle(TenantSettingsQuery request, CancellationToken cancellationToken)
    {
        var tenantSettings = await _context.TenantSettings.OrderBy(e => e.Setting.Name)
                                                          .ProjectTo<TenantSettingDto>(_mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);
        return new ListResult<TenantSettingDto>(tenantSettings);
    }
}
