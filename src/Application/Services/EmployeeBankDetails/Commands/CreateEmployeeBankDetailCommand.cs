namespace Engage.Application.Services.EmployeeBankDetails.Commands;

public class CreateEmployeeBankDetailCommand : EmployeeBankDetailCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeBankDetailCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeBankDetailCommand, OperationStatus>
{
    public CreateEmployeeBankDetailCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeBankDetailCommand command, CancellationToken cancellationToken)
    {
        if (command.IsPrimary == true)
        {
            var existingPrimaryAccount = await _context.EmployeeBankDetails
                                                    .Where(c => c.EmployeeId == command.EmployeeId
                                                        && c.IsPrimary == true && c.Deleted == false)
                                                    .FirstOrDefaultAsync();

            if (existingPrimaryAccount != null)
            {
                throw new UniqueException("This Employee Already has a Primary Bank Account. \n\n It can't be added again.");
            }
        }

        var entity = _mapper.Map<CreateEmployeeBankDetailCommand, EmployeeBankDetail>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeBankDetails.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeBankDetailId;
        return opStatus;
    }
}
