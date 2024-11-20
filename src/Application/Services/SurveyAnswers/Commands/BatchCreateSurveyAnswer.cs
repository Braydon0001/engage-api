using Engage.Application.Services.SurveyAnswerOptions.Commands;
using Engage.Application.Services.SurveyAnswerPhotos.Commands;

namespace Engage.Application.Services.SurveyAnswers.Commands;

public class BatchCreateSurveyAnswer
{
    public int SurveyQuestionId { get; set; }
    public int? QuestionFalseReasonId { get; set; }
    public string Answer { get; set; }
    public List<int> Options { get; set; }
    public List<BatchCreateSurveyAnswerPhoto> Photos { get; set; }
}

public class BatchCreateSurveyAnswerCommand : IRequest<OperationStatus>
{
    public int SurveyInstanceId { get; set; }
    public string SurveyPhotoFolder { get; set; }
    public string SurveyPhotoFolderPath { get; set; }
    public List<BatchCreateSurveyAnswer> Answers { get; set; }
}

public class BatchCreateSurveyAnswerCommandHandler : BaseUpdateCommandHandler, IRequestHandler<BatchCreateSurveyAnswerCommand, OperationStatus>
{
    public BatchCreateSurveyAnswerCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(BatchCreateSurveyAnswerCommand request, CancellationToken cancellationToken)
    {
        if (request.Answers != null && request.Answers.Count > 0)
        {
            foreach (var answer in request.Answers)
            {
                if (answer != null)
                {
                    var opStatus = await _mediator.Send(new CreateSurveyAnswerCommand()
                    {
                        SurveyInstanceId = request.SurveyInstanceId,
                        SurveyQuestionId = answer.SurveyQuestionId,
                        QuestionFalseReasonId = answer.QuestionFalseReasonId,
                        Answer = answer.Answer,
                    });

                    if (opStatus.Status)
                    {
                        var surveyAnswerId = (int)opStatus.OperationId;

                        if (answer.Options != null && answer.Options.Count > 0)
                        {
                            await _mediator.Send(new BatchCreateSurveyAnswerOptionCommand()
                            {
                                SurveyAnswerId = surveyAnswerId,
                                Options = answer.Options
                            });
                        }

                        if (answer.Photos != null && answer.Photos.Count > 0)
                        {
                            await _mediator.Send(new BatchCreateSurveyAnswerPhotoCommand()
                            {
                                SurveyAnswerId = surveyAnswerId,
                                SurveyPhotoFolder = request.SurveyPhotoFolder,
                                SurveyPhotoFolderPath = request.SurveyPhotoFolderPath,
                                Photos = answer.Photos
                            });
                        }
                    }
                }
                
            }
        }
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
