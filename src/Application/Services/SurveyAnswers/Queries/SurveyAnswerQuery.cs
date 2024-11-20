using Engage.Application.Services.SurveyAnswers.Models;

namespace Engage.Application.Services.SurveyAnswers.Queries;

public class SurveyAnswerQuery : GetByIdQuery, IRequest<StoreSurveyAnswerDto>
{
}

public class SurveyAnswerQueryHandler : BaseQueryHandler, IRequestHandler<SurveyAnswerQuery, StoreSurveyAnswerDto>
{
    public SurveyAnswerQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreSurveyAnswerDto> Handle(SurveyAnswerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SurveyAnswers.SingleAsync(x => x.SurveyAnswerId == request.Id, cancellationToken);
        return _mapper.Map<SurveyAnswer, StoreSurveyAnswerDto>(entity);
    }
}
