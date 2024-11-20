namespace Engage.Application.Services.TrainingProviders.Commands;

public class UpdateTrainingProviderCommand : TrainingProviderCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateTrainingProviderCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateTrainingProviderCommand, OperationStatus>
{
    public UpdateTrainingProviderCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateTrainingProviderCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.TrainingProviders.SingleAsync(x => x.TrainingProviderId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingProviderId;
        return opStatus;
    }
}
