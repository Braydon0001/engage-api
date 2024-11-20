namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarSurveyFormQuery : IRequest<EmployeeStoreCalendarSurveySubmissionVm>
{
    public int Id { get; set; }
}
public record EmployeeStoreCalendarSurveyFormHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeStoreCalendarSurveyFormQuery, EmployeeStoreCalendarSurveySubmissionVm>
{
    public async Task<EmployeeStoreCalendarSurveySubmissionVm> Handle(EmployeeStoreCalendarSurveyFormQuery query, CancellationToken cancellationToken)
    {
        var surveyInstance = await Context.SurveyFormSubmissions.AsNoTracking()
                                                                .Include(e => e.SurveyForm)
                                                                .FirstOrDefaultAsync(e => e.SurveyFormSubmissionId == query.Id, cancellationToken);

        var surveyQuestionGroups = await Context.SurveyFormQuestionGroups.AsNoTracking()
                                                                         .Where(e => e.SurveyFormId == surveyInstance.SurveyFormId)
                                                                         .ProjectTo<EmployeeStoreCalendarSurveyFormSubmissionGroupVm>(Mapper.ConfigurationProvider)
                                                                         .ToListAsync(cancellationToken);

        var questionGroupIds = surveyQuestionGroups.Select(e => e.Id).ToList();

        var surveyAnswerQuestions = await Context.SurveyFormAnswers.AsNoTracking()
                                                                   .Include(e => e.SurveyFormReason)
                                                                   .Include(e => e.SurveyFormAnswerOptions)
                                                                   .ThenInclude(e => e.SurveyFormOption)
                                                                   .Include(e => e.SurveyFormQuestion)
                                                                   .ThenInclude(e => e.SurveyFormQuestionType)
                                                                   .Include(e => e.SurveyFormQuestion)
                                                                   .ThenInclude(e => e.SurveyFormQuestionOptions)
                                                                   .ThenInclude(e => e.SurveyFormOption)
                                                                   .Include(e => e.SurveyFormQuestion)
                                                                   .ThenInclude(e => e.SurveyFormQuestionReasons)
                                                                   .ThenInclude(e => e.SurveyFormReason)
                                                                   .Where(e => e.SurveyFormSubmissionId == query.Id)
                                                                   .ProjectTo<EmployeeStoreCalendarQuestionAnswerVm>(Mapper.ConfigurationProvider)
                                                                   .ToListAsync(cancellationToken);

        var questions = await Context.SurveyFormQuestions.AsNoTracking()
                                                         .Include(e => e.SurveyFormQuestionType)
                                                         .Include(e => e.SurveyFormQuestionOptions)
                                                         .ThenInclude(e => e.SurveyFormOption)
                                                         .Include(e => e.SurveyFormQuestionReasons)
                                                         .ThenInclude(e => e.SurveyFormReason)
                                                         .Where(e => questionGroupIds.Contains(e.SurveyFormQuestionGroupId))
                                                         .ProjectTo<EmployeeStoreCalendarQuestionAnswerVm>(Mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

        foreach (var group in surveyQuestionGroups)
        {
            var questionIds = surveyAnswerQuestions.Where(e => e.SurveyFormQuestionGroupId == group.Id).Select(e => e.Id).ToList();
            group.Questions = surveyAnswerQuestions.Where(e => questionIds.Contains(e.Id)).ToList();
            group.Questions.AddRange(questions.Where(e => e.SurveyFormQuestionGroupId == group.Id && !questionIds.Contains(e.Id)).ToList());
            group.Questions = group.Questions.Where(e => e.Disabled == false).OrderBy(e => e.DisplayOrder).ToList();
        }

        var mappedEntity = Mapper.Map<EmployeeStoreCalendarSurveySubmissionVm>(surveyInstance);

        mappedEntity.Groups = surveyQuestionGroups.Where(e => e.Questions.Count > 0).ToList();

        //foreach (var questionGroup in mappedEntity.Groups)
        //{
        //    questionGroup.Questions = questionGroup.Questions.Where(e => e.Disabled == false).OrderBy(e => e.DisplayOrder).ToList();
        //}

        mappedEntity.Groups = [.. mappedEntity.Groups.OrderBy(e => e.DisplayOrder)];

        return mappedEntity;
    }
}