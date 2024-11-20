using Engage.Application.Services.Settings.Models;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.Settings.Queries;

public class SupplierSettingsQuery : IRequest<ListResult<SupplierSettingDto>>
{
    public int? SupplierId { get; set; }
}

public class SupplierSettingsQueryHandler : BaseQueryHandler, IRequestHandler<SupplierSettingsQuery, ListResult<SupplierSettingDto>>
{
    private readonly IMediator _mediator;
    private readonly IUserService _user;
    private readonly IMultiTenantContextAccessor _multiTenantContext;

    public SupplierSettingsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IUserService user, IMultiTenantContextAccessor multiTenantContext) : base(context, mapper)
    {
        _mediator = mediator;
        _user = user;
        _multiTenantContext = multiTenantContext;
    }

    public async Task<ListResult<SupplierSettingDto>> Handle(SupplierSettingsQuery request, CancellationToken cancellationToken)
    {
        var supplierSettings = await _context.SupplierSettings.IgnoreQueryFilters().Where(e => !e.Deleted && !e.Disabled) //ignore the tenant filter for the implicit include of Setting
                                                              .Where(e => e.SupplierId == (request.SupplierId ?? _user.SupplierId)
                                                                          && EF.Property<string>(e, "TenantId") == _multiTenantContext.MultiTenantContext.TenantInfo.Id) //manually filter by tenant
                                                              .OrderBy(e => e.Setting.Name)
                                                              .ProjectTo<SupplierSettingDto>(_mapper.ConfigurationProvider)
                                                              .ToListAsync(cancellationToken);
        var supplierSettingIds = supplierSettings.Select(e => e.SettingId);

        // Fall back to a tenant setting if there is no supplier setting.
        var tenantSettings = await _mediator.Send(new TenantSettingsQuery(), cancellationToken);
        var fallbackTenantSettings = tenantSettings.Data.Where(e => !supplierSettingIds.Contains(e.SettingId))
                                                        .Select(e => new SupplierSettingDto
                                                        {
                                                            SettingId = e.SettingId,
                                                            SettingName = e.SettingName,
                                                            NormalizedSettingName = e.NormalizedSettingName,
                                                            Value = e.Value,
                                                        })
                                                        .ToList();
        if (fallbackTenantSettings.Count > 0)
        {
            supplierSettings.AddRange(fallbackTenantSettings);
            supplierSettings = supplierSettings.OrderBy(e => e.SettingName).ToList();
        }

        return new ListResult<SupplierSettingDto>(supplierSettings);

    }
}
