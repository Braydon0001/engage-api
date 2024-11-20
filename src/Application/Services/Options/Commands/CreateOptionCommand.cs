namespace Engage.Application.Services.Options.Commands;

public class CreateOptionCommand : OptionCommand, IRequest<OperationStatus>
{

}

public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public CreateOptionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(CreateOptionCommand command, CancellationToken cancellationToken)
    {
        OptionUtils.Add(command.OptionType, command.Name, command.Description, _context);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = opStatus.OperationId;
        return opStatus;
    }
}
