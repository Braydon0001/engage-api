namespace Engage.Application.Services.GlAdjustments.Commands;

public class CreateGLAdjustmentCommand : GLAdjustmentCommand, IRequest<OperationStatus>
{
}

public class CreateGLAdjustmentCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateGLAdjustmentCommand, OperationStatus>
{
    public CreateGLAdjustmentCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateGLAdjustmentCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateGLAdjustmentCommand, GLAdjustment>(command);
        _context.GLAdjustments.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.GLAdjustmentId;
        return opStatus;
    }
}
