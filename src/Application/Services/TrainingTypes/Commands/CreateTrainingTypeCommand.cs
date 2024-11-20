namespace Engage.Application.Services.TrainingTypes.Commands;

public class CreateTrainingTypeCommand : TrainingTypeCommand, IRequest<OperationStatus>
{
}

public class CreateTrainingTypeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateTrainingTypeCommand, OperationStatus>
{
    public CreateTrainingTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateTrainingTypeCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateTrainingTypeCommand, TrainingType>(command);
        _context.TrainingTypes.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingTypeId;
        return opStatus;
    }
}
