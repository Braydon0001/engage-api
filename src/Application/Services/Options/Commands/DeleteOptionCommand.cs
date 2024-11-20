namespace Engage.Application.Services.Options.Commands;

public class DeleteOptionCommand : IRequest<OperationStatus>
{
    public string Option { get; set; }
    public int Id { get; set; }
    public bool Toggle { get; set; } = false;
}

public class DeleteOptionHandler : IRequestHandler<DeleteOptionCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public DeleteOptionHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await OptionUtils.FindAsync(request.Id, request.Option, _context, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(request.Option, request.Id);
        }

        if (request.Toggle)
        {
            entity.Disabled = !entity.Disabled;
        }
        else
        {
            entity.Deleted = true;
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}
