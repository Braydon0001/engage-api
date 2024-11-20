namespace Engage.Application.Services.SurveyAnswers.Commands;

public class CreateSurveyAnswerCommand : SurveyAnswerCommand, IRequest<OperationStatus>
{
    public int SurveyInstanceId { get; set; }
    public int SurveyQuestionId { get; set; }
}

public class CreateSurveyAnswerCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyAnswerCommand, OperationStatus>
{
    public CreateSurveyAnswerCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateSurveyAnswerCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSurveyAnswerCommand, SurveyAnswer>(command);
        entity.SurveyInstanceId = command.SurveyInstanceId;
        entity.SurveyQuestionId = command.SurveyQuestionId;
        _context.SurveyAnswers.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.SurveyAnswerId;
        return opStatus;
    }
}
