namespace Engage.Application.Services.Settings.Commands;

public class UpsertSupplierSettingCommand : IRequest<OperationStatus>
{
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

public class UpsertSupplierSettingCommandHandler : IRequestHandler<UpsertSupplierSettingCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public UpsertSupplierSettingCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(UpsertSupplierSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = await _context.Settings.IgnoreQueryFilters().SingleAsync(e => e.Name.ToLower() == request.Name.ToLower() && !e.Disabled && !e.Disabled, cancellationToken);

        var supplierSetting = await _context.SupplierSettings.SingleOrDefaultAsync(e => e.SupplierId == request.SupplierId &&
                                                                                        e.SettingId == setting.SettingId, cancellationToken);
        if (supplierSetting == null)
        {
            _context.SupplierSettings.Add(new SupplierSetting
            {
                SupplierId = request.SupplierId,
                SettingId = setting.SettingId,
                Value = request.Value
            });

            var operationStatus = await _context.SaveChangesAsync(cancellationToken);
            return operationStatus;
        }
        else
        {
            supplierSetting.Value = request.Value;

            var operationStatus = await _context.SaveChangesAsync(cancellationToken);
            operationStatus.OperationId = supplierSetting.SupplierSettingId;
            return operationStatus;
        }
    }
}
