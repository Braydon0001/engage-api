namespace Engage.Application.Services.GlAdjustments.Commands;

public class UpdateGLAdjustmentCommand : GLAdjustmentCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateGLAdjustmentCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateGLAdjustmentCommand, OperationStatus>
{
    public UpdateGLAdjustmentCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateGLAdjustmentCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.GLAdjustments.SingleAsync(x => x.GLAdjustmentId == command.Id);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.GLAdjustmentId;
        return opStatus;
    }
}
