namespace Engage.Application.Services.GlAccountTypes.Commands;

public class CreateGLAccountTypeCommand : GLAccountTypeCommand, IRequest<OperationStatus>
{

}

public class CreateGLAccountTypeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateGLAccountTypeCommand, OperationStatus>
{
    public CreateGLAccountTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateGLAccountTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateGLAccountTypeCommand, GLAccountType>(command);
        _context.GLAccountTypes.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.GLAccountTypeId;
        return opStatus;
    }
}
