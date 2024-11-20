using Engage.Application.Services.SurveyFormQuestions.Commands;

namespace Engage.Application.Services.SurveyFormQuestionReasons.Commands;

public class SurveyFormQuestionReasonBatchUpdateCommand : IRequest<OperationStatus>
{
    public int SurveyFormQuestionId { get; init; }
    public List<ReasonOption> AnswerReasons { get; init; }
}

public record SurveyFormQuestionReasonBatchUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionReasonBatchUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionReasonBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command.AnswerReasons != null)
        {
            //get the newly added reasons - where the id is 0
            var reasonsToAdd = command.AnswerReasons.Where(e => e.Id == 0).ToList();

            if (reasonsToAdd.Any())
            {
                foreach (var reason in reasonsToAdd)
                {
                    var surveyFormReason = new SurveyFormReason() { Name = reason.Text, CompleteSurvey = reason.CompleteSurvey };
                    Context.SurveyFormReasons.Add(surveyFormReason);
                    var surveyFormQuestionReason = new SurveyFormQuestionReason() { SurveyFormReason = surveyFormReason, SurveyFormQuestionId = command.SurveyFormQuestionId };
                    Context.SurveyFormQuestionReasons.Add(surveyFormQuestionReason);
                }
            }
        }

        //get the current answer reasons for this question
        var currentReasons = await Context.SurveyFormQuestionReasons.Include(e => e.SurveyFormReason).Where(e => e.SurveyFormQuestionId == command.SurveyFormQuestionId).ToListAsync(cancellationToken);

        var reasonsToRemove = new List<SurveyFormQuestionReason>();

        //if we have currentReasons but nothing in the command, we are removing everything
        if (currentReasons.Any() && (command.AnswerReasons == null || !command.AnswerReasons.Any()))
        {
            reasonsToRemove = currentReasons;
        }
        else if (command.AnswerReasons != null && command.AnswerReasons.Any())
        {
            reasonsToRemove = currentReasons.Where(e => !command.AnswerReasons.Select(e => e.Id).ToList().Contains(e.SurveyFormReason.SurveyFormReasonId)).ToList();
        }

        if (reasonsToRemove.Any())
        {
            Context.SurveyFormQuestionReasons.RemoveRange(reasonsToRemove);
        }

        var reasonsToUpdate = command.AnswerReasons != null ? command.AnswerReasons.Where(e => e.Id != 0).ToList() : [];

        if (reasonsToUpdate.Any())
        {
            foreach (var reason in reasonsToUpdate)
            {
                var entity = await Context.SurveyFormReasons.Where(e => e.SurveyFormReasonId == reason.Id).FirstOrDefaultAsync(cancellationToken);
                if (entity != null)
                {
                    entity.Name = reason.Text;
                    entity.CompleteSurvey = reason.CompleteSurvey;
                }
            }
        }

        //save
        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class SurveyFormQuestionReasonBatchUpdateValidator : AbstractValidator<SurveyFormQuestionReasonBatchUpdateCommand>
{
    public SurveyFormQuestionReasonBatchUpdateValidator()
    {
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.AnswerReasons);
    }
}