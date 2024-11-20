namespace Engage.Application.Services.Settings.Commands;

public class UpsertTenantSettingCommand : IRequest<OperationStatus>
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public class UpsertTenantSettingCommandHandler : IRequestHandler<UpsertTenantSettingCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public UpsertTenantSettingCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(UpsertTenantSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = await _context.Settings.SingleAsync(e => e.Name == request.Name, cancellationToken);

        var tenantSetting = await _context.TenantSettings.SingleOrDefaultAsync(e => e.SettingId == setting.SettingId &&
                                                                                    e.Value == request.Value, cancellationToken);
        if (tenantSetting == null)
        {
            _context.TenantSettings.Add(new TenantSetting
            {
                SettingId = setting.SettingId,
                Value = request.Value
            });
        }
        else
        {
            tenantSetting.Value = request.Value;
        }

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = tenantSetting.TenantSettingId;
        return operationStatus;
    }
}
