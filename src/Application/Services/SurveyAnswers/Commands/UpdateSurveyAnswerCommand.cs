namespace Engage.Application.Services.SurveyAnswers.Commands;

public class UpdateSurveyAnswerCommand : SurveyAnswerCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class UpdateSurveyAnswerCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSurveyAnswerCommand, OperationStatus>
{
    public UpdateSurveyAnswerCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(UpdateSurveyAnswerCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyAnswers.SingleAsync(x => x.SurveyAnswerId == command.Id);
        _mapper.Map(command, entity);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.SurveyInstanceId;
            return opStatus;
        }

        return new OperationStatus
        {
            Status = true
        };
    }
}
