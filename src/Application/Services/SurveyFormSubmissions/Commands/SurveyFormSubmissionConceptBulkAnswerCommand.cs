
namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormSubmissionConceptBulkAnswerCommand : IRequest<List<ReturnConceptAnswers>>
{
    public int SurveyFormSubmissionId { get; set; }
    public List<SurveyFormSubmissionAnswer> Answers { get; set; }
}

public class SurveyFormSubmissionAnswer
{
    public int SurveyFormQuestionId { get; set; }
    public int? SurveyFormReasonId { get; set; }
    public List<int> AnswerOptions { get; set; }
    public string AnswerText { get; set; }
    public DateTime AnswerDate { get; set; }
    public int? SurveyFormAnswerId { get; set; }
}

public record SurveyFormSubmissionConceptBulkAnswerHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormSubmissionConceptBulkAnswerCommand, List<ReturnConceptAnswers>>
{
    public async Task<List<ReturnConceptAnswers>> Handle(SurveyFormSubmissionConceptBulkAnswerCommand command, CancellationToken cancellationToken)
    {
        List<SurveyFormAnswer> answerList = [];
        if (command.Answers.IsNotNullOrEmpty())
        {
            foreach (var answer in command.Answers)
            {
                SurveyFormAnswer answerEntity;
                if (answer.SurveyFormAnswerId.HasValue)
                {
                    answerEntity = await Context.SurveyFormAnswers.IgnoreQueryFilters()
                                                      .Include(e => e.SurveyFormAnswerOptions)
                                                      .ThenInclude(e => e.SurveyFormOption)
                                                      .FirstOrDefaultAsync(e => e.SurveyFormAnswerId == answer.SurveyFormAnswerId, cancellationToken);
                }
                else
                {
                    answerEntity = await Context.SurveyFormAnswers.IgnoreQueryFilters()
                                                .Include(e => e.SurveyFormAnswerOptions)
                                                .ThenInclude(e => e.SurveyFormOption)
                                                .FirstOrDefaultAsync(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId
                                                     && e.SurveyFormQuestionId == answer.SurveyFormQuestionId, cancellationToken);
                }

                //Question hasn't been answered yet
                if (answerEntity == null)
                {
                    answerEntity = new SurveyFormAnswer()
                    {
                        SurveyFormSubmissionId = command.SurveyFormSubmissionId,
                        SurveyFormQuestionId = answer.SurveyFormQuestionId,
                        AnswerText = answer.AnswerText,
                        AnswerUuid = "",
                        SurveyFormReasonId = answer.SurveyFormReasonId,
                        AnswerDate = answer.AnswerDate,
                    };
                    Context.SurveyFormAnswers.Add(answerEntity);
                    if (answer.AnswerOptions != null)
                    {
                        foreach (var option in answer.AnswerOptions)
                        {
                            var answerOption = new SurveyFormAnswerOption()
                            {
                                SurveyFormAnswer = answerEntity,
                                SurveyFormOptionId = option
                            };
                            Context.SurveyFormAnswerOptions.Add(answerOption);
                        }
                    }
                    await Context.SaveChangesAsync(cancellationToken);
                    answerList.Add(answerEntity);
                }
                else
                {
                    //TODO: Update
                    if (answerEntity.Deleted && (answer.AnswerText != "" || answer.SurveyFormReasonId != null))
                    {
                        answerEntity.Deleted = false;
                    }
                    answerEntity.AnswerText = answer.AnswerText;
                    answerEntity.SurveyFormReasonId = answer.SurveyFormReasonId;
                    answerEntity.AnswerDate = answer.AnswerDate;

                    if (answer.AnswerText == "" && answer.SurveyFormReasonId == null)
                    {
                        answerEntity.Deleted = true;
                    }

                    var currentOptions = answerEntity.SurveyFormAnswerOptions.ToList();

                    var optionsToRemove = new List<SurveyFormAnswerOption>();
                    // if we have existing options but nothing in the command, we are removing everything
                    if (currentOptions.Any() && (answer.AnswerOptions == null || !answer.AnswerOptions.Any()))
                    {
                        optionsToRemove = currentOptions;
                    }
                    else if (answer.AnswerOptions != null && answer.AnswerOptions.Count > 0)
                    {
                        List<int> optionIds = [.. answer.AnswerOptions];

                        optionsToRemove = currentOptions.Where(e => !optionIds.Contains(e.SurveyFormOption.SurveyFormOptionId)).ToList();
                    }
                    if (optionsToRemove.Count > 0)
                    {
                        Context.SurveyFormAnswerOptions.RemoveRange(optionsToRemove);
                    }

                    //Adding options
                    var optionsToAdd = new List<int>();

                    if (answer.AnswerOptions != null && answer.AnswerOptions.Any())
                    {
                        List<int> optionIds = [.. answer.AnswerOptions];

                        optionsToAdd = optionIds.Where(e => !currentOptions.Select(e => e.SurveyFormOptionId).ToList().Contains(e)).ToList();
                    }
                    if (optionsToAdd.Count > 0)
                    {
                        foreach (var option in optionsToAdd)
                        {
                            var answerOption = new SurveyFormAnswerOption()
                            {
                                SurveyFormAnswerId = answerEntity.SurveyFormAnswerId,
                                SurveyFormOptionId = option,
                            };
                            Context.SurveyFormAnswerOptions.Add(answerOption);
                        }
                    }

                    await Context.SaveChangesAsync(cancellationToken);
                    answerList.Add(answerEntity);
                }
            }
        }

        List<ReturnConceptAnswers> returnObject = [];

        foreach (var answer in answerList)
        {
            returnObject.Add(Mapper.Map<ReturnConceptAnswers>(answer));
        }

        return returnObject;
    }
}

public class ReturnConceptAnswers : IMapFrom<SurveyFormAnswer>
{
    public int SurveyFormQuestionId { get; set; }
    public int SurveyFormAnswerId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, ReturnConceptAnswers>();
    }
}