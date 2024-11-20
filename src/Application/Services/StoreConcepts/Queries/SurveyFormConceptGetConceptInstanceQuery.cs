namespace Engage.Application.Services.StoreConcepts.Queries;

public class SurveyFormConceptGetConceptInstanceQuery : IRequest<SurveyFormConceptInstanceVm>
{
    public int StoreId { get; set; }
    public int StoreConceptId { get; set; }
}

public record SurveyFormConceptGetConceptInstanceHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormConceptGetConceptInstanceQuery, SurveyFormConceptInstanceVm>
{
    public async Task<SurveyFormConceptInstanceVm> Handle(SurveyFormConceptGetConceptInstanceQuery query, CancellationToken cancellationToken)
    {
        var surveyInstance = await Context.SurveyForms.AsNoTracking()
                                                          .Include(e => e.SurveyFormQuestionGroups)
                                                          .ThenInclude(e => e.SurveyFormQuestions)
                                                          .ThenInclude(e => e.SurveyFormQuestionType)
                                                          .Include(e => e.SurveyFormQuestionGroups)
                                                          .ThenInclude(e => e.SurveyFormQuestions)
                                                          .ThenInclude(e => e.SurveyFormQuestionOptions)
                                                          .ThenInclude(e => e.SurveyFormOption)
                                                          .Include(e => e.SurveyFormQuestionGroups)
                                                          .ThenInclude(e => e.SurveyFormQuestions)
                                                          .ThenInclude(e => e.SurveyFormQuestionReasons)
                                                          .ThenInclude(e => e.SurveyFormReason)
                                                          .ProjectTo<SurveyFormConceptInstanceVm>(Mapper.ConfigurationProvider)
                                                          .FirstOrDefaultAsync(e => e.Id == query.StoreConceptId, cancellationToken);

        foreach (var questionGroup in surveyInstance.Groups)
        {

            questionGroup.Questions = questionGroup.Questions.OrderBy(e => e.DisplayOrder).ToList();
        }

        surveyInstance.Groups = surveyInstance.Groups.OrderBy(e => e.DisplayOrder).ToList();

        return surveyInstance;

    }
}