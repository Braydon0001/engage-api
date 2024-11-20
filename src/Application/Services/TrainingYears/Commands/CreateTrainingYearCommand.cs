namespace Engage.Application.Services.TrainingYears.Commands;

public class CreateTrainingYearCommand : TrainingYearCommand, IRequest<OperationStatus>
{

}

public class CreateTrainingYearCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateTrainingYearCommand, OperationStatus>
{
    public CreateTrainingYearCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateTrainingYearCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateTrainingYearCommand, TrainingYear>(command);
        _context.TrainingYears.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingYearId;
        return opStatus;
    }
}
