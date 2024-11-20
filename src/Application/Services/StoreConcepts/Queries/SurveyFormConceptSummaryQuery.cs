namespace Engage.Application.Services.StoreConcepts.Queries;

public class SurveyFormConceptSummaryQuery : IRequest<SurveyFormConcepSummaryVm>
{
    public int Id { get; set; }
    public bool FullSurvey { get; set; } = false;
}
public record SurveyFormConceptSummaryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormConceptSummaryQuery, SurveyFormConcepSummaryVm>
{
    public async Task<SurveyFormConcepSummaryVm> Handle(SurveyFormConceptSummaryQuery query, CancellationToken cancellationToken)
    {
        var entity = await Context.SurveyFormSubmissions.AsNoTracking()
                                                        .Where(e => e.SurveyFormSubmissionId == query.Id)
                                                        .Include(e => e.SurveyForm)
                                                        .Include(e => e.SurveyFormAnswers)
                                                        .ThenInclude(e => e.SurveyFormQuestion)
                                                        .ThenInclude(e => e.SurveyFormQuestionType)
                                                        .Include(e => e.SurveyFormAnswers)
                                                        .ThenInclude(e => e.SurveyFormAnswerOptions)
                                                        .ThenInclude(e => e.SurveyFormOption)
                                                        .Include(e => e.SurveyFormAnswers)
                                                        .ThenInclude(e => e.SurveyFormQuestion)
                                                        .ThenInclude(e => e.SurveyFormQuestionGroup)
                                                        .Include(e => e.SurveyFormAnswers)
                                                        .ThenInclude(e => e.SurveyFormReason)
                                                        .FirstOrDefaultAsync(cancellationToken);

        var questionIds = entity.SurveyFormAnswers.Select(e => e.SurveyFormQuestionId).ToList();

        var questionGroupIds = entity.SurveyFormAnswers.Select(e => e.SurveyFormQuestion.SurveyFormQuestionGroupId).ToList();

        var questionOptions = await Context.SurveyFormQuestionOptions.AsNoTracking()
                                                                     .Where(e => questionIds.Contains(e.SurveyFormQuestionId))
                                                                     .Include(e => e.SurveyFormOption)
                                                                     .ToListAsync(cancellationToken);

        var answerReasons = await Context.SurveyFormQuestionReasons.AsNoTracking()
                                                                   .Where(e => questionIds.Contains(e.SurveyFormQuestionId))
                                                                   .Include(e => e.SurveyFormReason)
                                                                   .ToListAsync(cancellationToken);

        var questions = await Context.SurveyFormQuestions.AsNoTracking()
                                                         .Where(e => questionGroupIds.Contains(e.SurveyFormQuestionGroupId)
                                                                && !questionIds.Contains(e.SurveyFormQuestionId))
                                                         .Include(e => e.SurveyFormQuestionOptions)
                                                         .ThenInclude(e => e.SurveyFormOption)
                                                         .ToListAsync(cancellationToken: cancellationToken);

        foreach (var answer in entity.SurveyFormAnswers)
        {
            answer.SurveyFormQuestion.SurveyFormQuestionOptions = questionOptions.Where(e => e.SurveyFormQuestionId == answer.SurveyFormQuestionId).ToList();
            answer.SurveyFormQuestion.SurveyFormQuestionReasons = answerReasons.Where(e => e.SurveyFormQuestionId == answer.SurveyFormQuestionId).ToList();
        }

        var mappedEntity = Mapper.Map<SurveyFormConcepSummaryVm>(entity);

        if (query.FullSurvey)
        {
            var questionGroups = await Context.SurveyFormQuestionGroups.AsNoTracking()
                                                                       .Where(e => e.SurveyFormId == entity.SurveyFormId)
                                                                       .Include(e => e.SurveyFormQuestions)
                                                                       .ThenInclude(e => e.SurveyFormQuestionType)
                                                                       .ToListAsync(cancellationToken);

            var unansweredQuestions = questionGroups.SelectMany(e => e.SurveyFormQuestions).Where(e => !questionIds.Contains(e.SurveyFormQuestionId)).ToList();
            var unansweredQuestionIds = unansweredQuestions.Select(e => e.SurveyFormQuestionId).ToList();

            var unansweredQuestionOptions = await Context.SurveyFormQuestionOptions.AsNoTracking()
                                                                     .Where(e => unansweredQuestionIds.Contains(e.SurveyFormQuestionId))
                                                                     .Include(e => e.SurveyFormOption)
                                                                     .ToListAsync(cancellationToken);

            var unansweredAnswerReasons = await Context.SurveyFormQuestionReasons.AsNoTracking()
                                                                       .Where(e => unansweredQuestionIds.Contains(e.SurveyFormQuestionId))
                                                                       .Include(e => e.SurveyFormReason)
                                                                       .ToListAsync(cancellationToken);

            //List<SurveyFormConcepSummaryQuestionVm> unansweredQuestionList = [];
            foreach (var question in unansweredQuestions)
            {
                question.SurveyFormQuestionOptions = unansweredQuestionOptions.Where(e => e.SurveyFormQuestionId == question.SurveyFormQuestionId).ToList();
                question.SurveyFormQuestionReasons = unansweredAnswerReasons.Where(e => e.SurveyFormQuestionId == question.SurveyFormQuestionId).ToList();
                question.SurveyFormQuestionGroup = questionGroups.FirstOrDefault(e => e.SurveyFormQuestionGroupId == question.SurveyFormQuestionGroupId);
                //unansweredQuestionList.Add(Mapper.Map<SurveyFormConcepSummaryQuestionVm>(question));
                mappedEntity.Answers.Add(Mapper.Map<SurveyFormConcepSummaryQuestionVm>(question));
            }
        }

        mappedEntity.Answers = mappedEntity.Answers.OrderBy(e => e.GroupDisplayOrder).ThenBy(x => x.DisplayOrder).ToList();
        return mappedEntity;
    }
}