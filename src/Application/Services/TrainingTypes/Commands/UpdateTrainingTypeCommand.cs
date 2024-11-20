namespace Engage.Application.Services.TrainingTypes.Commands;

public class UpdateTrainingTypeCommand : TrainingTypeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateTrainingTypeCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateTrainingTypeCommand, OperationStatus>
{
    public UpdateTrainingTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateTrainingTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingTypes.SingleAsync(x => x.TrainingTypeId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingTypeId;
        return opStatus;
    }
}
