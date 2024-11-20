using Engage.Application.Services.SurveyFormQuestions.Commands;

namespace Engage.Application.Services.SurveyFormQuestionOptions.Commands;

public class SurveyFormQuestionOptionBatchUpdateCommand : IRequest<OperationStatus>
{
    public int SurveyFormQuestionId { get; init; }
    public List<ReasonOption> AnswerOptions { get; init; }
}

public record SurveyFormQuestionOptionBatchUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionOptionBatchUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(SurveyFormQuestionOptionBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command.AnswerOptions != null)
        {
            //get the newly added options - where the id is 0
            var optionsToAdd = command.AnswerOptions.Where(e => e.Id == 0).ToList();

            if (optionsToAdd.Any())
            {
                foreach (var option in optionsToAdd)
                {
                    var surveyFormOption = new SurveyFormOption() { Name = option.Text, CompleteSurvey = option.CompleteSurvey };
                    Context.SurveyFormOptions.Add(surveyFormOption);
                    var surveyFormQuestionOption = new SurveyFormQuestionOption() { SurveyFormOption = surveyFormOption, SurveyFormQuestionId = command.SurveyFormQuestionId };
                    Context.SurveyFormQuestionOptions.Add(surveyFormQuestionOption);
                }
            }
        }

        //get the current answer options for this question
        var currentOptions = await Context.SurveyFormQuestionOptions.Include(e => e.SurveyFormOption).Where(e => e.SurveyFormQuestionId == command.SurveyFormQuestionId).ToListAsync(cancellationToken);

        var optionsToRemove = new List<SurveyFormQuestionOption>();

        //if we have currentOptions but nothing in the command, we are removing everything
        if (currentOptions.Any() && (command.AnswerOptions == null || !command.AnswerOptions.Any()))
        {
            optionsToRemove = currentOptions;
        }
        else if (command.AnswerOptions != null && command.AnswerOptions.Any())
        {
            optionsToRemove = currentOptions.Where(e => !command.AnswerOptions.Select(e => e.Id).ToList().Contains(e.SurveyFormOption.SurveyFormOptionId)).ToList();
        }

        if (optionsToRemove.Any())
        {
            Context.SurveyFormQuestionOptions.RemoveRange(optionsToRemove);
        }

        var optionsToUpdate = command.AnswerOptions.Where(e => e.Id != 0).ToList();

        if (optionsToUpdate.Any())
        {
            foreach (var option in optionsToUpdate)
            {
                var entity = await Context.SurveyFormOptions.Where(e => e.SurveyFormOptionId == option.Id).FirstOrDefaultAsync(cancellationToken);
                if (entity != null)
                {
                    entity.Name = option.Text;
                    entity.CompleteSurvey = option.CompleteSurvey;
                }
            }
        }

        //save
        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}

public class SurveyFormQuestionOptionBatchUpdateValidator : AbstractValidator<SurveyFormQuestionOptionBatchUpdateCommand>
{
    public SurveyFormQuestionOptionBatchUpdateValidator()
    {
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.AnswerOptions);
    }
}