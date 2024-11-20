namespace Engage.Application.Services.GlAccountTypes.Commands;

public class UpdateGLAccountTypeCommand : GLAccountTypeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateGLAccountTypeCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateGLAccountTypeCommand, OperationStatus>
{
    public UpdateGLAccountTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateGLAccountTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.GLAccountTypes.SingleAsync(x => x.GLAccountTypeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.GLAccountTypeId;
        return opStatus;
    }
}
