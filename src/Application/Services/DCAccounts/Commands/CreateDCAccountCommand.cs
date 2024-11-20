namespace Engage.Application.Services.DCAccounts.Commands;

public class CreateDCAccountCommand : DCAccountCommand, IRequest<OperationStatus>
{
}

public class CreateDCAccountCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateDCAccountCommand, OperationStatus>
{
    public CreateDCAccountCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateDCAccountCommand command, CancellationToken cancellationToken)
    {
        var existingPrimaryAccount = await _context.DCAccounts
                                                    .Where(c => c.StoreId == command.StoreId
                                                        && c.IsPrimary == true && c.Deleted == false)
                                                    .FirstOrDefaultAsync();

        if (command.IsPrimary == true && existingPrimaryAccount != null)
        {
            existingPrimaryAccount.IsPrimary = false;
        }
        else
        {
            if (existingPrimaryAccount == null)
            {
                command.IsPrimary = true;
            }
        }

        var entity = _mapper.Map<CreateDCAccountCommand, DCAccount>(command);
        _context.DCAccounts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.DCAccountId;
        return opStatus;
    }
}
