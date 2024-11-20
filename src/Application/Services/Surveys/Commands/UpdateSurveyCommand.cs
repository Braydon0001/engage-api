namespace Engage.Application.Services.Surveys.Commands;

public class UpdateSurveyCommand : SurveyCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateSurveyCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSurveyCommand, OperationStatus>
{
    public UpdateSurveyCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }
    public async Task<OperationStatus> Handle(UpdateSurveyCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Surveys.SingleAsync(x => x.SurveyId == command.Id);

        if (entity == null)
            throw new NotFoundException(nameof(Survey), command.Id);
        
        _mapper.Map(command, entity);

        await SurveyAssigns.BatchAssign(_mediator, command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.SurveyId;
        return opStatus;
    }
}
