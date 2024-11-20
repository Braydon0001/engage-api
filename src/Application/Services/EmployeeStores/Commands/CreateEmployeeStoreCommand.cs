namespace Engage.Application.Services.EmployeeStores.Commands;

public class CreateEmployeeStoreCommand : EmployeeStoreCommand, IRequest<OperationStatus>
{
}

public class CreateEmployeeStoreCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeStoreCommand, OperationStatus>
{
    public CreateEmployeeStoreCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateEmployeeStoreCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEmployeeStoreCommand, EmployeeStore>(command);
        _context.EmployeeStores.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeStoreId;
        return opStatus;
    }
}
