namespace Engage.Application.Services.Options.Commands;

public class UpdateOptionCommand : OptionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool Deleted { get; set; }
    public bool Disabled { get; set; }
}

public class UpdateOptionCommandHandler : IRequestHandler<UpdateOptionCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public UpdateOptionCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(UpdateOptionCommand command, CancellationToken cancellationToken)
    {
        var option = await OptionUtils.FindAsync(command.Id, command.OptionType, _context, cancellationToken);

        option.Name = command.Name;
        option.Description = command.Description;
        option.Deleted = command.Deleted;
        option.Disabled = command.Disabled;

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.Id;
        return opStatus;
    }
}
