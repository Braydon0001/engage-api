using Engage.Application.Services.SurveyAnswers.Models;

namespace Engage.Application.Services.SurveyAnswers.Queries;

public class SurveyAnswerRuleQuestionQuery : IRequest<SurveyAnswerWebQuestionDto>
{
    public int Id { get; set; }
}
public class SurveyAnswerRuleQuestionHandler : VmQueryHandler, IRequestHandler<SurveyAnswerRuleQuestionQuery, SurveyAnswerWebQuestionDto>
{
    public SurveyAnswerRuleQuestionHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SurveyAnswerWebQuestionDto> Handle(SurveyAnswerRuleQuestionQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SurveyAnswers
            .AsQueryable()
            .Include(e => e.SurveyAnswerOptions)
            .Include(e => e.QuestionFalseReason)
            .AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyAnswerId == request.Id, cancellationToken);

        var mappedEntity = _mapper.Map<SurveyAnswerWebQuestionDto>(entity);

        if (mappedEntity == null)
        {
            return null;
        }

        //check if answer is boolean question
        if (mappedEntity.Answer.Equals("True", StringComparison.OrdinalIgnoreCase)
            || mappedEntity.Answer.Equals("False", StringComparison.OrdinalIgnoreCase))
        {
            mappedEntity.BooleanAnswer = mappedEntity.Answer.Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;
        }

        if (entity.SurveyAnswerOptions.Any())
        {
            List<OptionDto> options = new List<OptionDto>();
            foreach (var item in entity.SurveyAnswerOptions)
            {
                var option = await _context.SurveyQuestionOptions
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.SurveyQuestionOptionId == item.SurveyQuestionOptionId);
                if (option == null)
                {
                }
                else
                {
                    options.Add(new OptionDto
                    {
                        Id = option.SurveyQuestionOptionId,
                        Name = option.Option,
                        Disabled = false
                    });
                }
            }
            mappedEntity.SurveyOptions = options;
        }

        return mappedEntity;
    }
}