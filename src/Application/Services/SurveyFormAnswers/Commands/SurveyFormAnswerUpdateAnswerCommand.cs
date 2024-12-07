﻿namespace Engage.Application.Services.SurveyFormAnswers.Commands;

public class SurveyFormAnswerUpdateAnswerCommand : IRequest<SurveyFormAnswer>, IMapTo<SurveyFormAnswer>
{
    public int? Id { get; set; }
    public int SurveyFormSubmissionId { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public List<int> AnswerOptions { get; set; }
    public int? SurveyFormReasonId { get; set; }
    public string AnswerText { get; set; }
    public DateTime AnswerDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswerUpdateAnswerCommand, SurveyFormAnswer>()
               .ForMember(d => d.AnswerUuid, opt => opt.MapFrom(s => ""));
    }
}
public record SurveyFormAnswerUpdateAnswerHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerUpdateAnswerCommand, SurveyFormAnswer>
{
    public async Task<SurveyFormAnswer> Handle(SurveyFormAnswerUpdateAnswerCommand command, CancellationToken cancellationToken)
    {
        //Answer Already Exists
        SurveyFormAnswer answerEntity;
        if (command.Id != null)
        {
            answerEntity = await Context.SurveyFormAnswers.IgnoreQueryFilters()
                                                      .Include(e => e.SurveyFormAnswerOptions)
            .ThenInclude(e => e.SurveyFormOption)
                                                      .FirstOrDefaultAsync(e => e.SurveyFormAnswerId == command.Id, cancellationToken);
        }
        else
        {
            answerEntity = await Context.SurveyFormAnswers.IgnoreQueryFilters()
                                                          .Include(e => e.SurveyFormAnswerOptions)
                                                          .ThenInclude(e => e.SurveyFormOption)
                                                          .FirstOrDefaultAsync(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId
                                                                    && e.SurveyFormQuestionId == command.SurveyFormQuestionId, cancellationToken);
        }

        // Question hasn't been answered yet.
        if (answerEntity == null)
        {
            var entity = Mapper.Map<SurveyFormAnswer>(command);

            Context.SurveyFormAnswers.Add(entity);

            await Context.SaveChangesAsync(cancellationToken);

            if (command.AnswerOptions.NotNullOrEmpty())
            {
                foreach (var answerOption in command.AnswerOptions)
                {
                    Context.SurveyFormAnswerOptions.Add(new SurveyFormAnswerOption
                    {
                        SurveyFormAnswerId = entity.SurveyFormAnswerId,
                        SurveyFormOptionId = answerOption
                    });
                }
                await Context.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        if (answerEntity.Deleted && (command.AnswerText != "" || command.SurveyFormReasonId != null))
        {
            answerEntity.Deleted = false;
        }

        answerEntity.AnswerText = command.AnswerText;
        answerEntity.SurveyFormReasonId = command.SurveyFormReasonId;
        answerEntity.AnswerDate = command.AnswerDate;

        if (command.AnswerText == null || command.AnswerText == "" && command.SurveyFormReasonId == null)
        {
            answerEntity.Deleted = true;
        }

        var currenOptions = answerEntity.SurveyFormAnswerOptions.ToList();

        if (currenOptions.NotNullOrEmpty())
        {
            List<int> optionsToDelete = [];
            if (command.AnswerOptions.IsNullOrEmpty())
            {
                optionsToDelete = currenOptions.Select(e => e.SurveyFormAnswerOptionId).ToList();
            }
            else
            {
                optionsToDelete = currenOptions.Where(e => !command.AnswerOptions.Contains(e.SurveyFormAnswerOptionId)).Select(e => e.SurveyFormAnswerOptionId).ToList();
            }
            if (optionsToDelete.Count > 0)
            {
                Context.SurveyFormAnswerOptions.RemoveRange(currenOptions.Where(e => optionsToDelete.Contains(e.SurveyFormAnswerOptionId)));
            }
        }

        if (command.AnswerOptions.IsNotNullOrEmpty())
        {
            foreach (var option in command.AnswerOptions)
            {
                Context.SurveyFormAnswerOptions.Add(new SurveyFormAnswerOption()
                {
                    SurveyFormAnswerId = answerEntity.SurveyFormAnswerId,
                    SurveyFormOptionId = option
                });
            }

            //Context.SurveyFormAnswerOptions.AddRange(command.AnswerOptions.Select(e => new SurveyFormAnswerOption()
            //{
            //    SurveyFormAnswerId = answerEntity.SurveyFormAnswerId,
            //    SurveyFormAnswerOptionId = e
            //}).ToList());
        }

        var submission = await Context.SurveyFormSubmissions
                                      .FirstOrDefaultAsync(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId, cancellationToken);

        if (submission != null && submission.IsComplete)
        {
            submission.IsComplete = false;
            submission.CompletedDate = null;
        }

        await Context.SaveChangesAsync(cancellationToken);

        return answerEntity;
    }
}
public class SurveyFormAnswerUpdateAnswerValidator : AbstractValidator<SurveyFormAnswerUpdateAnswerCommand>
{
    public SurveyFormAnswerUpdateAnswerValidator()
    {
        RuleFor(e => e.SurveyFormQuestionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormSubmissionId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.AnswerDate).NotNull();
    }
}