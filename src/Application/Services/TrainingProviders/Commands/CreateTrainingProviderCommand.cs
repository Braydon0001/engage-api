namespace Engage.Application.Services.TrainingProviders.Commands;

public class CreateTrainingProviderCommand : TrainingProviderCommand, IRequest<OperationStatus>
{
}

public class CreateTrainingProviderCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateTrainingProviderCommand, OperationStatus>
{
    public CreateTrainingProviderCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateTrainingProviderCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateTrainingProviderCommand, TrainingProvider>(command);
        _context.TrainingProviders.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingProviderId;
        return opStatus;
    }
}
