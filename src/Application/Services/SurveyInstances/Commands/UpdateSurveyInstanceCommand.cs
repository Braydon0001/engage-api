namespace Engage.Application.Services.SurveyInstances.Commands;

public class UpdateEmployeeStoreSurveyCommand : SurveyInstanceCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeStoreSurveyCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeStoreSurveyCommand, OperationStatus>
{
    public UpdateEmployeeStoreSurveyCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeStoreSurveyCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyInstances.SingleAsync(x => x.SurveyInstanceId == command.Id, cancellationToken);

        return await SaveChangesAsync(command, entity, nameof(SurveyInstance), command.Id, cancellationToken);
    }
}
