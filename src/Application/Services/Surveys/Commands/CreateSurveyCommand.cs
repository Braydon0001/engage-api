namespace Engage.Application.Services.Surveys.Commands;

public class CreateSurveyCommand : SurveyCommand, IRequest<OperationStatus>
{
}

public class CreateSurveyCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyCommand, OperationStatus>
{
    public CreateSurveyCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(CreateSurveyCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSurveyCommand, Survey>(command);
        _context.Surveys.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.SurveyId;
            await SurveyAssigns.BatchAssign(_mediator, command, entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return opStatus;
    }
}
