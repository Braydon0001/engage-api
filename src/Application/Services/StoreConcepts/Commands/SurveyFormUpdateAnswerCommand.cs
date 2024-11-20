
namespace Engage.Application.Services.StoreConcepts.Commands;

public class SurveyFormUpdateAnswerCommand : IRequest<SurveyFormAnswerHistory>
{
    public int Id { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public List<int> AnswerOptions { get; set; }
    public int? AnswerReasonId { get; set; }
    public string AnswerText { get; set; }
}
public record SurveyFormUpdateAnswerHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormUpdateAnswerCommand, SurveyFormAnswerHistory>
{
    public async Task<SurveyFormAnswerHistory> Handle(SurveyFormUpdateAnswerCommand command, CancellationToken cancellationToken)
    {
        var currentAnswer = await Context.SurveyFormAnswers.Where(e => e.SurveyFormAnswerId == command.Id)
                                                           .Include(e => e.SurveyFormAnswerOptions)
                                                           .FirstOrDefaultAsync(cancellationToken)
                                                           ?? throw new Exception("No Answer Found");

        SurveyFormAnswerHistory history = new()
        {
            SurveyFormAnswerId = currentAnswer.SurveyFormAnswerId,
            SurveyFormReasonId = currentAnswer.SurveyFormReasonId,
            AnswerText = currentAnswer.AnswerText,
        };

        Context.SurveyFormAnswerHistories.Add(history);

        await Context.SaveChangesAsync(cancellationToken);

        foreach (var answerOption in currentAnswer.SurveyFormAnswerOptions)
        {
            Context.SurveyFormAnswerOptionHistories.Add(new()
            {
                SurveyFormAnswerHistoryId = history.SurveyFormAnswerHistoryId,
                SurveyFormOptionId = answerOption.SurveyFormOptionId
            });
        }
        //update current answer

        currentAnswer.AnswerText = command.AnswerText;
        currentAnswer.SurveyFormReasonId = command.AnswerReasonId;

        if (currentAnswer.SurveyFormAnswerOptions.IsNotNullOrEmpty() || command.AnswerOptions.IsNotNullOrEmpty())
        {
            var currentOptions = currentAnswer.SurveyFormAnswerOptions.ToList();
            if (currentAnswer.SurveyFormAnswerOptions.IsNullOrEmpty())
            {
                foreach (var answerOption in command.AnswerOptions)
                {
                    Context.SurveyFormAnswerOptions.Add(new()
                    {
                        SurveyFormAnswerId = currentAnswer.SurveyFormAnswerId,
                        SurveyFormOptionId = answerOption
                    });
                }
            }

            if (currentAnswer.SurveyFormAnswerOptions.IsNotNullOrEmpty())
            {
                List<int> optionsToDelete = [];
                if (command.AnswerOptions.IsNullOrEmpty())
                {
                    optionsToDelete = currentAnswer.SurveyFormAnswerOptions.Select(e => e.SurveyFormOptionId).ToList();
                }
                else
                {
                    optionsToDelete = currentAnswer.SurveyFormAnswerOptions
                                                   .Where(e => !command.AnswerOptions.Contains(e.SurveyFormOptionId))
                                                   .Select(e => e.SurveyFormAnswerOptionId)
                                                   .ToList();
                }

                if (optionsToDelete.Count > 0)
                {
                    Context.SurveyFormAnswerOptions.RemoveRange(currentOptions.Where(e => optionsToDelete.Contains(e.SurveyFormAnswerOptionId)));
                }
            }

            if (command.AnswerOptions.IsNotNullOrEmpty())
            {
                var newOptions = command.AnswerOptions
                                                    .Where(e => !currentAnswer.SurveyFormAnswerOptions
                                                                .Select(f => f.SurveyFormOptionId).ToList().Contains(e)
                                                          )
                                                    .ToList();
                foreach (var answerOption in newOptions)
                {
                    Context.SurveyFormAnswerOptions.Add(new()
                    {
                        SurveyFormAnswerId = currentAnswer.SurveyFormAnswerId,
                        SurveyFormOptionId = answerOption
                    });
                }
            }
        }

        await Context.SaveChangesAsync(cancellationToken);

        return history;
    }
}

public class SurveyFormUpdateAnswerValidator : AbstractValidator<SurveyFormUpdateAnswerCommand>
{
    public SurveyFormUpdateAnswerValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.AnswerText).NotEmpty();
    }
}