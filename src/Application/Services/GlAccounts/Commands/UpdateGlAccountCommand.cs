namespace Engage.Application.Services.GLAccounts.Commands;

public class UpdateGLAccountCommand : GLAccountCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateGLAccountCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateGLAccountCommand, OperationStatus>
{
    public UpdateGLAccountCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateGLAccountCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.GLAccounts.SingleAsync(x => x.GLAccountId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.GLAccountId;
        return opStatus;
    }
}
