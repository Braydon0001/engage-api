namespace Engage.Application.Services.EmployeeStores.Commands;

public class CreateEmployeeStoresByStoreCommand : IRequest<OperationStatus>
{
    public int StoreId { get; set; }
    public List<int> EmployeeId { get; set; }
    public List<int> EngageSubGroupId { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }
}

public class CreateEmployeeStoresByStoreCommandHandler : IRequestHandler<CreateEmployeeStoresByStoreCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public CreateEmployeeStoresByStoreCommandHandler(IAppDbContext context) 
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(CreateEmployeeStoresByStoreCommand command, CancellationToken cancellationToken)
    {
        var employeeSubGroups = await EmployeeSubGroups.Calculate(command.StoreId, command.EmployeeId, command.EngageSubGroupId, _context, cancellationToken);

        foreach (var employeeSubGroup in employeeSubGroups)
        {
            _context.EmployeeStores.Add(new EmployeeStore
            {
                StoreId = command.StoreId ,
                EmployeeId = employeeSubGroup.EmployeeId,
                EngageSubGroupId = employeeSubGroup.EngageSubGroupId,
                FrequencyTypeId = command.FrequencyTypeId,
                Frequency = command.Frequency,
                Note = command.Note,
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }   
}
