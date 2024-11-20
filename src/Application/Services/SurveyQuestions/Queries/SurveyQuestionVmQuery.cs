using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.SurveyQuestions.Queries;

public class SurveyQuestionVmQuery : GetByIdQuery, IRequest<SurveyQuestionVm>
{
}

public class SurveyQuestionVmQueryHandler : BaseQueryHandler, IRequestHandler<SurveyQuestionVmQuery, SurveyQuestionVm>
{
    public SurveyQuestionVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyQuestionVm> Handle(SurveyQuestionVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyQuestions.Include(x => x.QuestionType)
                                    .Include(x => x.EngageVariantProduct)
                                    .Include(x => x.SurveyQuestionFalseReasons)
                                    .ThenInclude(x => x.QuestionFalseReason)
                                    .Include(x => x.SurveyQuestionOptions)
                                    .Include(x => x.Rules)
                                    .SingleAsync(x => x.SurveyQuestionId == request.Id, cancellationToken);

        var question = _mapper.Map<SurveyQuestion, SurveyQuestionVm>(entity);

        if (entity.SurveyQuestionOptions.Count > 0)
        {
            int optionNo = 1;
            foreach (var option in entity.SurveyQuestionOptions)
            {
                if (optionNo == 1)
                {
                    question.Option1Id = option.SurveyQuestionOptionId;
                    question.Option1 = option.Option;
                    question.CompleteSurvey1 = option.CompleteSurvey;
                }
                if (optionNo == 2)
                {
                    question.Option2Id = option.SurveyQuestionOptionId;
                    question.Option2 = option.Option;
                    question.CompleteSurvey2 = option.CompleteSurvey;
                }
                if (optionNo == 3)
                {
                    question.Option3Id = option.SurveyQuestionOptionId;
                    question.Option3 = option.Option;
                    question.CompleteSurvey3 = option.CompleteSurvey;
                }
                if (optionNo == 4)
                {
                    question.Option4Id = option.SurveyQuestionOptionId;
                    question.Option4 = option.Option;
                    question.CompleteSurvey4 = option.CompleteSurvey;
                }
                if (optionNo == 5)
                {
                    question.Option5Id = option.SurveyQuestionOptionId;
                    question.Option5 = option.Option;
                    question.CompleteSurvey5 = option.CompleteSurvey;
                }
                if (optionNo == 6)
                {
                    question.Option6Id = option.SurveyQuestionOptionId;
                    question.Option6 = option.Option;
                    question.CompleteSurvey6 = option.CompleteSurvey;
                }
                if (optionNo == 7)
                {
                    question.Option7Id = option.SurveyQuestionOptionId;
                    question.Option7 = option.Option;
                    question.CompleteSurvey7 = option.CompleteSurvey;
                }
                if (optionNo == 8)
                {
                    question.Option8Id = option.SurveyQuestionOptionId;
                    question.Option8 = option.Option;
                    question.CompleteSurvey8 = option.CompleteSurvey;
                }
                if (optionNo == 9)
                {
                    question.Option9Id = option.SurveyQuestionOptionId;
                    question.Option9 = option.Option;
                    question.CompleteSurvey9 = option.CompleteSurvey;
                }
                if (optionNo == 10)
                {
                    question.Option10Id = option.SurveyQuestionOptionId;
                    question.Option10 = option.Option;
                    question.CompleteSurvey10 = option.CompleteSurvey;
                }
                optionNo++;
            }
        }

        return question;
    }
}
