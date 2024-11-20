namespace Engage.Application.Services.SurveyAnswerOptions.Commands;

public class BatchCreateSurveyAnswerOption
{
    public int? SurveyAnswerOptionId { get; set; }
    public int SurveyQuestionOptionId { get; set; }
}

public class BatchCreateSurveyAnswerOptionCommand : IRequest<OperationStatus>
{
    public int SurveyAnswerId { get; set; }
    public List<int> Options { get; set; }
}

public class BatchCreateSurveyAnswerOptionCommandHandler : BaseCreateCommandHandler, IRequestHandler<BatchCreateSurveyAnswerOptionCommand, OperationStatus>
{
    public BatchCreateSurveyAnswerOptionCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(BatchCreateSurveyAnswerOptionCommand command, CancellationToken cancellationToken)
    {
        foreach (var option in command.Options)
        {
            await _mediator.Send(new CreateSurveyAnswerOptionCommand()
            {
                SurveyAnswerId = command.SurveyAnswerId,
                SurveyQuestionOptionId = option,
                SaveChanges = false
            });
        }

        return new OperationStatus()
        {
            Status = true
        };
    }
}
