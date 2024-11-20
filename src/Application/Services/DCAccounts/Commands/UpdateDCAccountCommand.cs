namespace Engage.Application.Services.DCAccounts.Commands;

public class UpdateDCAccountCommand : DCAccountCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateDCAccountCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateDCAccountCommand, OperationStatus>
{
    public UpdateDCAccountCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateDCAccountCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.DCAccounts.SingleAsync(x => x.DCAccountId == command.Id, cancellationToken);
        var primaryExists = await _context.DCAccounts
                                            .Where(c => c.StoreId == entity.StoreId && c.IsPrimary == true && c.DCAccountId != entity.DCAccountId)
                                            .FirstOrDefaultAsync();
        if (command.IsPrimary)
        {
            if (entity.IsPrimary == false)
            {
                if (primaryExists != null)
                {
                    primaryExists.IsPrimary = false;
                }
            }
        }
        else
        {
            command.IsPrimary = primaryExists == null ? true : false;
        }

        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.DCAccountId;
        return opStatus;
    }
}
