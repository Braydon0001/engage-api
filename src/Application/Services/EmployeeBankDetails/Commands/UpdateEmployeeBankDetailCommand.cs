namespace Engage.Application.Services.EmployeeBankDetails.Commands;

public class UpdateEmployeeBankDetailCommand : EmployeeBankDetailCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateBankDetailCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeBankDetailCommand, OperationStatus>
{
    public UpdateBankDetailCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeBankDetailCommand command, CancellationToken cancellationToken)
    {
        if (command.IsPrimary)
        {
            var originalAccount = await _context.EmployeeBankDetails
                                            .Where(c => c.EmployeeBankDetailId == command.Id)
                                            .FirstOrDefaultAsync();

            if (originalAccount.IsPrimary == false)
            {
                var mainExists = await _context.EmployeeBankDetails
                                            .Where(c => c.EmployeeId == originalAccount.EmployeeId && c.IsPrimary == true)
                                            .FirstOrDefaultAsync();

                if (mainExists != null)
                {
                    throw new ClaimException("This Employee already has a Primary Bank Account. \n\n It can't be added again.");
                }
            }
        }

        var entity = await _context.EmployeeBankDetails.SingleAsync(x => x.EmployeeBankDetailId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeBankDetailId;
        return opStatus;
    }
}
