using Microsoft.EntityFrameworkCore.DynamicLinq;

namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormSubmissionAnswerCommand : IMapTo<SurveyFormSubmission>, IRequest<SurveyFormAnswer>
{
    public int? SurveyFormSubmissionId { get; set; }
    //public int? EmployeeId { get; init; }
    //public int SurveyFormId { get; init; }
    //public int? StoreId { get; init; }
    public string SubmissionUuid { get; init; }
    public int SurveyFormQuestionId { get; init; }
    //public List<JsonSetting> Metadata { get; init; }
    public string AnswerText { get; init; }
    public int? SurveyFormReasonId { get; init; }
    public List<string> AnswerOptions { get; init; }
    public string AnswerUuid { get; set; }
    public int? AnswerId { get; set; }
    public DateTime AnswerDate { get; set; }
    public bool SendNotification { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmissionAnswerCommand, SurveyFormAnswer>();
    }
}

public record SurveyFormSubmissionAnswerHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<SurveyFormSubmissionAnswerCommand, SurveyFormAnswer>
{
    public async Task<SurveyFormAnswer> Handle(SurveyFormSubmissionAnswerCommand command, CancellationToken cancellationToken)
    {
        // we first have to get the submission entity to do all the necessary linking. We assume that it already exists as the survey had to have been started
        SurveyFormSubmission submissionEntity;
        if (command.SurveyFormSubmissionId == null)
        {
            submissionEntity = await Context.SurveyFormSubmissions.Include(s => s.SurveyForm).ThenInclude(f => f.SurveyFormType).FirstOrDefaultAsync(e => e.SubmissionUuid == command.SubmissionUuid, cancellationToken);
        }
        else
        {
            submissionEntity = await Context.SurveyFormSubmissions.Include(s => s.SurveyForm).ThenInclude(f => f.SurveyFormType).FirstOrDefaultAsync(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId && e.SubmissionUuid == command.SubmissionUuid, cancellationToken);
        }

        if (submissionEntity == null)
        {
            return null;
        }

        // once we have the submission entity, we must determine if the answer already exists
        SurveyFormAnswer answerEntity;
        if (command.AnswerId == null)
        {
            answerEntity = await Context.SurveyFormAnswers.IgnoreQueryFilters()
                                        .Include(e => e.SurveyFormAnswerOptions)
                                            .ThenInclude(e => e.SurveyFormOption)
                                        .FirstOrDefaultAsync(e => e.AnswerUuid == command.AnswerUuid, cancellationToken);
        }
        else
        {
            answerEntity = await Context.SurveyFormAnswers.IgnoreQueryFilters()
                                        .Include(e => e.SurveyFormAnswerOptions)
                                            .ThenInclude(e => e.SurveyFormOption)
                                        .FirstOrDefaultAsync(e => e.SurveyFormAnswerId == command.AnswerId && command.AnswerUuid == e.AnswerUuid, cancellationToken);
        }

        // if we don't have an answerEntity, we are adding a new answer
        if (answerEntity == null)
        {
            var surveyAnswer = new SurveyFormAnswer()
            {
                SurveyFormSubmissionId = submissionEntity.SurveyFormSubmissionId,
                SurveyFormQuestionId = command.SurveyFormQuestionId,
                AnswerText = command.AnswerText,
                AnswerUuid = command.AnswerUuid,
                SurveyFormReasonId = command.SurveyFormReasonId,
                AnswerDate = command.AnswerDate,
            };
            Context.SurveyFormAnswers.Add(surveyAnswer);
            if (command.AnswerOptions != null)
            {
                foreach (var option in command.AnswerOptions)
                {
                    var optionId = Int32.TryParse(option, out int n) ? n : (int?)null;
                    if (optionId != null)
                    {
                        var answerOption = new SurveyFormAnswerOption()
                        {
                            SurveyFormAnswer = surveyAnswer,
                            SurveyFormOptionId = option.ToInt32(),
                        };
                        Context.SurveyFormAnswerOptions.Add(answerOption);
                    }
                }
            }
            await Context.SaveChangesAsync(cancellationToken);

            if (submissionEntity.SurveyForm.SurveyFormType.Name == "POS Update" && command.SendNotification == true)
            {
                await Mediator.Send(new SurveyFormSubmissionEmailCommand
                {
                    SurveyFormSubmissionId = submissionEntity.SurveyFormSubmissionId,
                    StoreId = submissionEntity.StoreId.Value,
                    EmployeeId = submissionEntity.EmployeeId.Value,
                    AnswerDate = command.AnswerDate.ToFullDateTimeString(),
                    //AttachmentUrl = answerEntity.Files[0].Url
                }, cancellationToken);
            }

            return surveyAnswer;
        }
        // otherwise we are updating a previous entry
        else
        {
            if (answerEntity.Deleted && (command.AnswerText != "" || command.SurveyFormReasonId != null))
            {
                answerEntity.Deleted = false;
            }
            answerEntity.AnswerText = command.AnswerText;
            answerEntity.SurveyFormReasonId = command.SurveyFormReasonId;
            answerEntity.AnswerDate = command.AnswerDate;
            //if we have an existing answer and the new answer is falsey for boith the answer and reason, the answer was "deleted"
            if (command.AnswerText == "" && command.SurveyFormReasonId == null)
            {
                answerEntity.Deleted = true;
            }

            var currentOptions = answerEntity.SurveyFormAnswerOptions.ToList();

            var optionsToRemove = new List<SurveyFormAnswerOption>();
            // if we have existing options but nothing in the command, we are removing everything
            if (currentOptions.Any() && (command.AnswerOptions == null || !command.AnswerOptions.Any()))
            {
                optionsToRemove = currentOptions;
            }
            else if (command.AnswerOptions != null && command.AnswerOptions.Any() && command.AnswerOptions.Any())
            {
                List<int> optionIds = command.AnswerOptions
                                      .Select(s => Int32.TryParse(s, out int n) ? n : (int?)null)
                                      .Where(n => n.HasValue)
                                      .Select(n => n.Value)
                                      .ToList();

                optionsToRemove = currentOptions.Where(e => !optionIds.Contains(e.SurveyFormOption.SurveyFormOptionId)).ToList();
            }
            if (optionsToRemove.Any())
            {
                Context.SurveyFormAnswerOptions.RemoveRange(optionsToRemove);
            }

            var optionsToAdd = new List<int>();

            if (command.AnswerOptions != null && command.AnswerOptions.Any())
            {
                List<int> optionIds = command.AnswerOptions
                                      .Select(s => Int32.TryParse(s, out int n) ? n : (int?)null)
                                      .Where(n => n.HasValue)
                                      .Select(n => n.Value)
                                      .ToList();

                optionsToAdd = optionIds.Where(e => !currentOptions.Select(e => e.SurveyFormOptionId).ToList().Contains(e)).ToList();
            }
            if (optionsToAdd.Any())
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

            if (submissionEntity.SurveyForm.SurveyFormType.Name == "POS Update" && command.SendNotification == true)
            {
                await Mediator.Send(new SurveyFormSubmissionEmailCommand
                {
                    SurveyFormSubmissionId = submissionEntity.SurveyFormSubmissionId,
                    StoreId = submissionEntity.StoreId.Value,
                    EmployeeId = submissionEntity.EmployeeId.Value,
                    AnswerDate = command.AnswerDate.ToFullDateTimeString(),
                    //AttachmentUrl = answerEntity.Files[0].Url
                }, cancellationToken);
            }

            return answerEntity;
        }
    }
}

public class SurveyFormSubmissionAnswerValidator : AbstractValidator<SurveyFormSubmissionAnswerCommand>
{
    public SurveyFormSubmissionAnswerValidator()
    {
        RuleFor(x => x.SurveyFormSubmissionId).NotEmpty().GreaterThan(0).Unless(e => e.SubmissionUuid != null);
        RuleFor(e => e.SubmissionUuid);
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.AnswerText);
        RuleFor(e => e.AnswerUuid).NotEmpty();
        RuleFor(e => e.AnswerDate).NotEmpty();
    }
}