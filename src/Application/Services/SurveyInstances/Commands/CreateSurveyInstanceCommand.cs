using Engage.Application.Services.SurveyAnswers.Commands;

namespace Engage.Application.Services.SurveyInstances.Commands;

public class CreateSurveyInstanceCommand : SurveyInstanceCommand, IRequest<OperationStatus>
{
    public string SurveyPhotoFolder { get; set; }
    public string SurveyPhotoFolderPath { get; set; }
    public List<BatchCreateSurveyAnswer> Answers { get; set; }
}

public class CreateSurveyInstanceCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyInstanceCommand, OperationStatus>
{
    public CreateSurveyInstanceCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(CreateSurveyInstanceCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSurveyInstanceCommand, SurveyInstance>(command);
        _context.SurveyInstances.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status && command.Answers != null && command.Answers.Count > 0)
        {
            var answersCommand = new BatchCreateSurveyAnswerCommand()
            {
                SurveyInstanceId = entity.SurveyInstanceId,
                SurveyPhotoFolder = command.SurveyPhotoFolder,
                SurveyPhotoFolderPath = command.SurveyPhotoFolderPath,
                Answers = command.Answers
            };
            await _mediator.Send(answersCommand, cancellationToken);
        }

        opStatus.OperationId = entity.SurveyInstanceId;
        return opStatus;
    }
}
