namespace Engage.Application.Services.EmployeeStores.Commands;

public class CopyEmployeeStoresCommand : IRequest<OperationStatus>
{
    public int FromEmployeeId { get; set; }
    public int ToEmployeeId { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }
}

public class CopyEmployeeStoresCommandHandler : IRequestHandler<CopyEmployeeStoresCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public CopyEmployeeStoresCommandHandler(IAppDbContext context) 
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(CopyEmployeeStoresCommand command, CancellationToken cancellationToken)
    {
        var storeSubGroups = await StoreSubGroups.Calculate(command.FromEmployeeId, command.ToEmployeeId, _context, cancellationToken);

        foreach (var storeSubGroup in storeSubGroups)
        {
            _context.EmployeeStores.Add(new EmployeeStore
            {
                EmployeeId = command.ToEmployeeId,
                StoreId = storeSubGroup.StoreId,
                EngageSubGroupId = storeSubGroup.EngageSubGroupId,
                FrequencyTypeId = command.FrequencyTypeId,
                Frequency = command.Frequency,
                Note = command.Note,
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }   
}
